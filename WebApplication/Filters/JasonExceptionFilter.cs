using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using WebApplication.Models;

namespace WebApplication.Filters
{

    public class JasonExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        
        public JasonExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;   
            }
            else
            {
                error.Message = "A Server Error Occurred";
                error.Detail = context.Exception.Message;
            }
            

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}