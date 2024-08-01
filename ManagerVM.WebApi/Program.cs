
using ManagerVM.Services;
using ManagerVM.Services.Helper;

namespace ManagerVM.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddAppService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}
            app.UseMiddleware<CustomExceptionHandlerMiddleware>(Array.Empty<object>());

            await app.InitDataAsync();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
