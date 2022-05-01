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

        public async Task<SystemUser> Get(int sysno)
        {
            var user=await _authCenterContext.SystemUsers.FindAsync(sysno);
            return user;
        }

        public async Task<int> Insert(SystemUser systemUser)
        {
            _authCenterContext.Add(systemUser);
            await _authCenterContext.SaveChangesAsync();
            return systemUser.SysNo;
        }
    }
}
