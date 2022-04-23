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
    public class MapWhenMiddleware
    {
        private readonly IOperationTransient _operationTransient;
        private readonly IOperationSingleton _operationSingleton;
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MapWhenMiddleware(RequestDelegate next,
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
            _logger.LogWarning("**********MapWhenMiddleware");
            await _next(context);
        }
    }




    public static class MapWhenMiddlewareExtensions
    {
        public static IApplicationBuilder UseMapWhenMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MapWhenMiddleware>();
        }
    }
}
