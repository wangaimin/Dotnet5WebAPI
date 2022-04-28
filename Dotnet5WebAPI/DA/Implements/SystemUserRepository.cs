using Dotnet5WebAPI.Microsoft.EntityFrameworkCore;
using Dotnet5WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.DA
{
    public class SystemUserRepository: ISystemUserRepository
    {
        AuthCenterContext _authCenterContext;
        public SystemUserRepository(AuthCenterContext authCenterContext)
        {
            _authCenterContext = authCenterContext;
        }

        public SystemUser Get(int sysno)
        {
            var user=_authCenterContext.SystemUsers.Find(sysno);
            return user;
        }
    }
}
