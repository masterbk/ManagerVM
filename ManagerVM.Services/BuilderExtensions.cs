using ManagerVM.Data.Helper;
using ManagerVM.Data;
using ManagerVM.Services.Helper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.Span;
using ManagerVM.Contacts.Models;

namespace ManagerVM.Services
{
    public static class BuilderExtensions
    {
        public static void AddAppService(this WebApplicationBuilder builder)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            const string root = "Config";
            builder.Configuration.AddJsonFile($"{root}/appsettings.json", false, true);
            builder.Configuration.AddJsonFile($"{root}/appsettings.{enviroment}.json", true, true);
            builder.Configuration.AddEnvironmentVariables();

            var nginxOptions = new MoodleConfig();
            builder.Configuration.GetSection(nameof(MoodleConfig)).Bind(nginxOptions);
            builder.Services.AddSingleton(nginxOptions);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            builder.Services.AddDbContext<VMDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(VMDbContext)), sql =>
            {
                sql.MigrationsHistoryTable("__EFMigrationsHistory", ServiceConstants.NAME);
            }));

            builder.AddCustomLogging();

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IOpenStackClient, OpenStackClient>();
            builder.Services.AddScoped<ILMSClient, LMSClient>();
            //builder.Services.AddHttpClient<IOpenStackClient, OpenStackClient>(options =>
            //{
            //    options.Timeout = TimeSpan.FromMinutes(30);
            //});

            //builder.Services.AddHttpClient<ILMSClient, LMSClient>(options =>
            //{
            //    //options.Timeout = TimeSpan.FromMinutes(30);
            //});

            builder.Services.AddSingleton<CurrentUserProvider>();
            builder.Services.AddSingleton<DateTimeProvider>();

            builder.AddCustomAuthorization();
            builder.AddCustomHangfireSQL(builder.Configuration.GetSection("HangFireConfig:ConnectionString").Get<string>(), ServiceConstants.NAME);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "My API - V1",
                        Version = "v1"
                    }
                 );

                c.CustomSchemaIds((Type type) => type.FullName);
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = ""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    new string[0]
                } });

                var xmlFile = $"ManagerVM.WebApi.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });
        }

        public static void AddCustomAuthorization(this WebApplicationBuilder builder)
        {

            var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void AddCustomHangfireSQL(this WebApplicationBuilder builder, string sqlConnectionString, string schemaName, int commandBatchMaxTimeout = 0, int slidingInvisibilityTimeout = 0, int workerCountPerProcessor = 0)
        {
            string sqlConnectionString2 = sqlConnectionString;
            string schemaName2 = schemaName;
            WebApplicationBuilder builder2 = builder;
            builder2.Services.AddHangfire(delegate (IGlobalConfiguration configuration)
            {
                configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180).UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(sqlConnectionString2, new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes((commandBatchMaxTimeout == 0) ? 5 : commandBatchMaxTimeout),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes((slidingInvisibilityTimeout == 0) ? 5 : slidingInvisibilityTimeout),
                        QueuePollInterval = TimeSpan.FromSeconds(15),
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true,
                        SchemaName = schemaName2 + ".HangFire"
                    })
                    .UseFilter(new HangFireExpirationTimeAttribute(builder2.Configuration));
            });

            builder2.Services.AddHangfireServer(delegate (BackgroundJobServerOptions options)
            {
                options.WorkerCount = Environment.ProcessorCount * ((workerCountPerProcessor == 0) ? 1 : workerCountPerProcessor);
            });
        }

        public static void AddCustomLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog(delegate (HostBuilderContext context, LoggerConfiguration loggerConfiguration)
            {
                loggerConfiguration.Enrich.WithSpan().Enrich.FromLogContext().Enrich.WithProperty("ServiceName", AppDomain.CurrentDomain.FriendlyName).ReadFrom.Configuration(context.Configuration);
            });

            builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }

        public static async Task InitDataAsync(this WebApplication webApplication)
        {
            using IServiceScope serviceScope = webApplication.Services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<VMDbContext>();

            dbContext.Database.Migrate();

            var supperAdmin = await dbContext.Users.FirstOrDefaultAsync(f => f.UserName.Equals("supperadmin"));
            if (supperAdmin == null)
            {
                var salt = Guid.NewGuid().ToString();
                await dbContext.Users.AddAsync(new Data.Entities.UserEntity
                {
                    UserName = "supperadmin",
                    Salt = salt,
                    Password = $"{salt}Vtc@123".ToSHA256Hash(),
                    IsAdmin = true
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
