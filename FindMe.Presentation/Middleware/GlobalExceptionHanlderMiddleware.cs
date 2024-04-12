using FindMe.Shared;
using FindMe.Shared.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Text.Json;


namespace FindMe.Presentation.Middleware
{
    public class GlobalExceptionHanlderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<GlobalExceptionHanlderMiddleware> _localization;
        public GlobalExceptionHanlderMiddleware(
            RequestDelegate next,
            IStringLocalizer<GlobalExceptionHanlderMiddleware> stringLocalizer)
        {
            _next = next;
            _localization = stringLocalizer;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {

                    var response = new Response();
                    response.IsSuccess = false;
                    response.Message = _localization["Unauthorize"].Value;
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    var jsonOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    var exceptionResult = JsonSerializer.Serialize(response, jsonOptions);


                    await context.Response.WriteAsync(exceptionResult);
                    return;
                }
                await _next(context);
            }
            catch (GlobalException ex)
            {
                await HandlingExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandlingExceptionAsync(context, ex);
            }
        }

        private static Task HandlingExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new Response();
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var exceptionResult = JsonSerializer.Serialize(response, jsonOptions);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            return context.Response.WriteAsync(exceptionResult);
        }

        private static Task HandlingExceptionAsync(HttpContext context, GlobalException exception)
        {
            var response = new Response() { IsSuccess = false };

            exception.HandleExceptionAsync(context, response);
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var exceptionResult = JsonSerializer.Serialize(response, jsonOptions);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
