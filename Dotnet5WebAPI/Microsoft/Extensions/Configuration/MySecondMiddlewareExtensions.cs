using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public class MySecondMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MySecondMiddleware(RequestDelegate next,
                            ILogger<MyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogWarning("Second Middleware Start" );
            await _next(context);
            _logger.LogWarning("Second Middleware End");
        }
    }

    public static class MySecondMiddlewareExtensions
    {
        public static IApplicationBuilder UseMySecondMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MySecondMiddleware>();
        }
    }
}
