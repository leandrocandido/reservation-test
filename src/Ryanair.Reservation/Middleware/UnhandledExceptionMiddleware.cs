using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Ryanair.Reservation.Domain.Validation;
using Ryanair.Reservation.DTO;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Ryanair.Reservation.Middleware
{
    public class UnhandledExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UnhandledExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<UnhandledExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _mapper = mapper;
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

                ExceptionDto dto;
                Type type; // used to xml serialization
                // For domain exceptions we return a 422 status meaning the entity received was unable to fulfill the requirements.
                if (ex.GetType() == typeof(DomainValidationException))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    dto = _mapper.Map<DomainValidationExceptionDto>(ex);
                    type = typeof(DomainValidationExceptionDto);
                }
                // Otherwise we just return a 500 status code as it's an unexpected exception.
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    dto = _mapper.Map<ExceptionDto>(ex);
                    type = typeof(ExceptionDto);
                }

                // On development mode we return the complete information about the error, but not in production.
                //if (!this._env.IsDevelopment())
                //   dto.StackTrace = null;

                string response = "";
                httpContext.Request.Headers.TryGetValue("Accept", out StringValues accept);

                if (accept[0] == "application/xml")
                {
                    httpContext.Response.ContentType = "application/xml";
                    response = SerializeObjectToXml(dto, type);
                }
                else
                {
                    httpContext.Response.ContentType = "application/json";
                    response = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
                }


                await httpContext.Response.WriteAsync(response);
                return;
            }

        }


        /// <summary>
        /// Serialize a [Serializable] object of type T into an XML/UTF8 string.
        /// </summary>
        /// <typeparam name="T">Default type used if no type sent.</typeparam>
        /// <param name="item">Object to be serialized.</param>
        /// <param name="type">The type of the object.</param>
        /// <returns></returns>
        private string SerializeObjectToXml<T>(T item, Type type = null)
        {
            try
            {
                using (var sww = new StringWriter())
                {
                    XmlSerializer xs = new XmlSerializer(type ?? typeof(T));
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xs.Serialize(writer, item);
                        return sww.ToString();
                    }
                }

                //using (MemoryStream stream = new MemoryStream())
                //{
                //    using (XmlTextWriter xml = new XmlTextWriter(stream, new UTF8Encoding(false)))
                //    {

                //        xs.Serialize(xml, item);
                //        return new UTF8Encoding().GetString(((MemoryStream)xml.BaseStream).ToArray());
                //    }
                //}
            }
            catch (Exception)
            {
                return string.Empty;
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
