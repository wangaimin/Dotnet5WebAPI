using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public class MyFirstMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MyFirstMiddleware(RequestDelegate next,
                            ILogger<MyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogWarning("First Middleware Start" );
            await _next(context);
            _logger.LogWarning("First Middleware End");
        }
    }

    public static class MyFirstMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyFirstMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyFirstMiddleware>();
        }
    }
}
