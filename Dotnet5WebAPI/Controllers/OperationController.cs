using Dotnet5WebAPI.Interface;
using Dotnet5WebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationTransient _operationTransient;
        private readonly IOperationScoped _operationScoped;
        private readonly IOperationSingleton _operationSingleton;
       // private readonly ILogger _logger;
        public OperationController(
                            IOperationTransient operationTransient,
                            IOperationScoped operationScoped,
                            IOperationSingleton operationSingletone)
        {
            _operationScoped = operationScoped;
            _operationSingleton = operationSingletone;
            _operationTransient = operationTransient;
          //  _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("operationTransient id=" + _operationTransient.OperationId);
            stringBuilder.AppendLine("operationTransient id=" + _operationTransient.OperationId);
            stringBuilder.AppendLine("operationScoped id=" + _operationScoped.OperationId);
            stringBuilder.AppendLine("operationScoped id=" + _operationScoped.OperationId);
            stringBuilder.AppendLine("_operationSingleton id=" + _operationSingleton.OperationId);
            stringBuilder.AppendLine("_operationSingleton id=" + _operationSingleton.OperationId);
            return stringBuilder.ToString();
        }
    }
}
