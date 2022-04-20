using Dotnet5WebAPI.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public class MyMiddleware
    {
        private readonly IOperationTransient _operationTransient;
        private readonly IOperationSingleton _operationSingleton;
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MyMiddleware(RequestDelegate next,
                            IOperationTransient operationTransient, 
                            IOperationSingleton operationSingleton,
                            ILogger<MyMiddleware> logger)
        {
            _operationSingleton = operationSingleton;
            _operationTransient = operationTransient;
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context,
      IOperationScoped scopedOperation)
        {
            _logger.LogWarning("Transient: " + _operationTransient.OperationId);
            _logger.LogWarning("Scoped: " + scopedOperation.OperationId);
            _logger.LogWarning("Singleton: " + _operationSingleton.OperationId);
            await _next(context);
        }
    }




    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
