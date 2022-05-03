using Dotnet5WebAPI.Models;
using Dotnet5WebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Controllers
{
    [Route("api/systemuser")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        private readonly ISystemUserService _systemUserService;
        public SystemUserController(ISystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<SystemUser> Get(int sysno)
        {
              return await _systemUserService.Get(sysno);
        }

        [HttpPut]
        [Route("Put")]
        public async Task<int> Put(SystemUser systemUser)
        {
            return await _systemUserService.Insert(systemUser);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<SystemUser> Post(SystemUser systemUser)
        {
            return await _systemUserService.Update(systemUser);
        }
    }
}
