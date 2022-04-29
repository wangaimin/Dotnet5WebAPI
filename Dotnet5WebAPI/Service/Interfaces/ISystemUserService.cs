﻿using Dotnet5WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Service
{
    public interface ISystemUserService
    {
         Task<SystemUser> Get(int sysno);
    }
}