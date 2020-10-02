using System;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddleware(RequestDelegate requestDelegate, IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception exception)
            {
                Log.Error(
                    $"An error occurred " +
                    $"{exception.Message} {exception.StackTrace} " +
                    $"{exception.InnerException} {exception.Source}"
                );

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var exceptionResponse = _hostEnvironment.IsDevelopment()
                    ? new APIException(StatusCodes.Status500InternalServerError, exception.Message)
                    : new APIResponse(StatusCodes.Status500InternalServerError);
                var jsonResponse = JsonSerializer.Serialize(exceptionResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                await httpContext.Response.WriteAsync(jsonResponse);
            }
        }
    }
}