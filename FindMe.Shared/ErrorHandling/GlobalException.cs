using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Shared.ErrorHandling
{
    public abstract class GlobalException : Exception
    {
        public GlobalException(string message) : base(message) { }
        public abstract Task HandleExceptionAsync(HttpContext context, Response response);
    }
}
