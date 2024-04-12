using Microsoft.AspNetCore.Http;

using System.Net;


namespace FindMe.Shared.ErrorHandling.Exceptions
{
    public class BadRequestException : GlobalException
    {
        private readonly string _message;
        public BadRequestException(string message) : base(message)
        {
            _message = message;
        }

        public override Task HandleExceptionAsync(HttpContext context, Response response)
        {
            response.Message = _message;
            response.StatusCode = HttpStatusCode.BadRequest;

            return Task.CompletedTask;
        }
    }
}
