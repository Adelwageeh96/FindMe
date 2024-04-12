using Microsoft.AspNetCore.Http;
using System.Net;


namespace FindMe.Shared.ErrorHandling.Exceptions
{
    public class ServerErrorException : GlobalException
    {
        private readonly string _message;
        public ServerErrorException(string message) : base(message)
        {
            _message = message;
        }

        public override Task HandleExceptionAsync(HttpContext context, Response response)
        {
            response.Message = _message;
            response.StatusCode = HttpStatusCode.InternalServerError;

            return Task.CompletedTask;
        }
    }
}
