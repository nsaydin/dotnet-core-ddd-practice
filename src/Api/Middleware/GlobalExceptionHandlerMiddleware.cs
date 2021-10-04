using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Application.Dtos;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                    return;

                ApiError apiError;
                switch (ex)
                {
                    case ValidationException validationException:
                        apiError = new ApiError
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = validationException.Errors.ToList()
                        };
                        break;

                    default:
                        apiError = new ApiError
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Errors = { ex.Message }
                        };
                        // if (!string.IsNullOrEmpty(ex.StackTrace))
                        //     apiError.Errors.Add(ex.StackTrace);
                        // if (ex.InnerException != null)
                        //     apiError.Errors.Add(ex.InnerException.Message);

                        break;
                }

                switch (apiError.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        _logger.LogWarning(ex, "ValidationError");
                        break;
                    default:
                        _logger.LogCritical(ex, "InternalServerError");
                        break;
                }
                
                var responseJson = JsonConvert.SerializeObject(apiError, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                });

                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = (int)apiError.StatusCode;
                await context.Response.WriteAsync(responseJson);
            }
        }
    }
}