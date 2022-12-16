using System.Net;
using System.Text.Json;
using Api.Errors;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware( RequestDelegate next,
                                    ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                //if we don't handle exceptions in any other places this is where is handled
                _logger.LogError(ex, ex.Message);

                await HandleError(context, _env, ex);
                
            }
        }

        public static async Task HandleError(HttpContext context, IHostEnvironment env,  Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment() 
                ? new ApiException(context.Response.StatusCode, 
                                    ex.Message, 
                                    ex.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode, 
                                    ex.Message, 
                                    "Internal Server Error");

            var options = new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            
            var jsonResponse = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}