using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Common
{
    public class MyExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var exception = context.Exception;
                var httpContext = context.HttpContext;
                if (exception is BusinessException businessException)
                {
                    httpContext.Response.StatusCode = 200;
                    context.Result = new ObjectResult(ApiResponse.Failed("data", businessException.Message));
                }
                else 
                {
                    httpContext.Response.StatusCode = 500;
                    context.Result = new ObjectResult(ApiResponse.Failed("data", exception.Message));
                }
                context.ExceptionHandled = true;
            }
           
        }
    }
}
