using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Common
{
    public class DataWrapperFilter : IAsyncResultFilter
    {
        DataWrapperOptions _options;
        public DataWrapperFilter()
        {
            _options = new DataWrapperOptions();
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode ?? context.HttpContext.Response.StatusCode;

                if (!_options.NoWrapStatusCode.Any(s => s == statusCode))
                {
                    var wrapperResult = WrapDataResult(objectResult.Value, context.HttpContext);
                    objectResult.Value = wrapperResult.WrapperResult;
                    objectResult.DeclaredType = wrapperResult.WrapperResult.GetType();

                    if (wrapperResult.ChangeHttpStatus)
                    {
                        objectResult.StatusCode = wrapperResult.RewriteHttpStatusCode.Value;
                    }
                }
            }
            await next();
        }

        private ObjectResultWrapperData WrapDataResult(object currentResult, HttpContext httpContext)
        {
            if (currentResult is IResultDataWrapper)
                return new ObjectResultWrapperData(currentResult, false);

            if (currentResult is ProblemDetails problemDetails)
            {
                if (!_options.WrapProblemDetails)
                    return new ObjectResultWrapperData(currentResult, false);

                int? stautsCode;
                bool changeHttpStauts = false;
                string failedMsg = string.Empty;
                if (currentResult is ValidationProblemDetails validationProblemDetails)
                {
                    failedMsg = string.Join("；", validationProblemDetails.Errors.SelectMany(s => s.Value));
                }

                failedMsg = string.IsNullOrEmpty(failedMsg) ? problemDetails.Title : failedMsg;

                if (_options.RewriteProblemDetailsResponseStatusCode.HasValue)
                {
                    changeHttpStauts = true;
                    stautsCode = _options.RewriteProblemDetailsResponseStatusCode.Value;
                }
                else
                {
                    stautsCode = problemDetails.Status ?? httpContext.Response.StatusCode;
                }

                return new ObjectResultWrapperData(ApiResponse.Failed(problemDetails.Detail, failedMsg, stautsCode.Value), changeHttpStauts, stautsCode);
            }

            return new ObjectResultWrapperData(ApiResponse.Succeed(currentResult), false);
        }

        public struct ObjectResultWrapperData
        {
            public object WrapperResult { get; set; }

            public bool ChangeHttpStatus { get; set; }

            public int? RewriteHttpStatusCode { get; set; }

            public ObjectResultWrapperData(object data, bool changeStatus, int? rewriteCode = null)
            {
                WrapperResult = data;
                ChangeHttpStatus = changeStatus;
                RewriteHttpStatusCode = rewriteCode;
            }
        }

    }
}
