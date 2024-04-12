using Microsoft.AspNetCore.Http;
using System.Net;


namespace FindMe.Shared.ErrorHandling.Exceptions
{
    public class KeyNotFoundException : GlobalException
    {
        private readonly string _message;
        public KeyNotFoundException(string message) : base(message)
        {
            _message = message;
        }

        public override async Task HandleExceptionAsync(HttpContext context, Response response)
        {
            response.Message = _message;
            response.StatusCode = HttpStatusCode.NotFound;

            await Task.CompletedTask;
        }
    }
}
