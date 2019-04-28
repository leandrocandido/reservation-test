using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ryanair.Reservation.Domain.Validation;
using System;
using System.Threading.Tasks;

namespace Ryanair.Reservation.Middleware
{
    public class UnhandledExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public UnhandledExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<UnhandledExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // For domain exceptions we return a 422 status meaning the entity received was unable to fulfill the requirements.
                if (ex.GetType() == typeof(DomainValidationException))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                }
                // Otherwise we just return a 500 status code as it's an unexpected exception.
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                // On development mode we return the complete information about the error.
                // But on production, just a generic message.
                // Check disabled for now.
                //if (this._env.IsDevelopment())
                //response.Error = ex.ToString();
                ////else
                ////response.Error = Language.AnUnhandledErrorOccurredOnServerSide;


                //httpContext.Response.ContentType = "application/json";

                //await httpContext.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(response));
                return;
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UnhandledExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseUnhandledExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnhandledExceptionMiddleware>();
        }
    }
}
