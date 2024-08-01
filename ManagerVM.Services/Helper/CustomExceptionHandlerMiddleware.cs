using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Helper
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly IAppLogger<CustomExceptionHandlerMiddleware> _logger;

        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(IAppLogger<CustomExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex2)
            {
                _logger.LogError(ex2, ex2.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    ex2.Message
                });
            }
        }
    }
}
