using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Controllers
{
    public class OperationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //todo:调用瞬时服务、范围服务、单例服务
    }
}
