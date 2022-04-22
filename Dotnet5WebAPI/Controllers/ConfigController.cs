using Dotnet5WebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ConfigService _configService;
        public ConfigController(ConfigService configService) {
            _configService = configService;
        }

        [HttpGet]
        public ActionResult<string> Get() 
        {
            _configService.Get_BindConfig();
            _configService.Get_GetConfig();
            return _configService.GetConfig();
        }
    }
}
