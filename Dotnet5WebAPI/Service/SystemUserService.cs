﻿using Dotnet5WebAPI.DA;
using Dotnet5WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Service
{
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserRepository _systemUserRepository;

        public SystemUserService(ISystemUserRepository systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
        }

        public async Task<SystemUser> Get(int sysno)
        {
            return await _systemUserRepository.Get(sysno);
        }

        public async Task<int> Insert(SystemUser systemUser)
        {
            await _systemUserRepository.Insert(systemUser);
            return systemUser.SysNo;
        }

        public async Task<SystemUser> Update(SystemUser systemUser)
        {
            var result=await _systemUserRepository.Update(systemUser);
            return result;
        }
    }
}
