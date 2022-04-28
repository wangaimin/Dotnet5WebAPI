using Dotnet5WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.DA
{
    public interface ISystemUserRepository
    {
        SystemUser Get(int sysno);
    }
}
