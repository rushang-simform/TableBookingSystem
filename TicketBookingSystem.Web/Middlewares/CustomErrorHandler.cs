using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.DTOs.Response;
using System;
using System.Threading.Tasks;

namespace TableBookingSystem.Web.Middlewares
{
    public class CustomErrorHandler : IMiddleware
    {
        private readonly ILogger _logger;

        public CustomErrorHandler(ILogger logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in request pipeline");
                await context.Response.WriteAsync(GenerateErrorResponse());
                context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            }
        }
        public string GenerateErrorResponse()
        {
            return GenericResponse<string>.Fail("Something went wrong").ToString();
        }
    }
    public static class CustomErrorHandlerExtensions
    {
        public static void UseCustomErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomErrorHandler>();
        }
    }
}
