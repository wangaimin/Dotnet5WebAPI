using Dotnet5WebAPI.DA;
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

        public SystemUser Get(int sysno)
        {
            return _systemUserRepository.Get(sysno);
        }
    }
}
