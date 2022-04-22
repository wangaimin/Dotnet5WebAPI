using Dotnet5WebAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Service
{
    /// <summary>
    /// 配置操作类
    /// 配置文件修改会事实生效
    /// </summary>
    public class ConfigService
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<ConfigService> Logger;
        public ConfigService(IConfiguration configuration, ILogger<ConfigService> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        public string GetConfig()
        {
            var title = $"title:{Configuration["Position:Title"]}";
            var name = $"name:{Configuration["Position:Name"]}";
            Logger.LogWarning(title);
            Logger.LogWarning(name);
            return title + " /n " + name;
        }

        public void Get_BindConfig()
        {
            var positionOptions = new PositionOptions();
            Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            var title = $"Get_BindConfig title:{positionOptions.Title}";
            var name = $"Get_BindConfig name:{positionOptions.Name}";
            Logger.LogWarning(title);
            Logger.LogWarning(name);
        }

        public void Get_GetConfig()
        {
            var positionOptions = Configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();
            var title = $"Get_GetConfig title:{positionOptions.Title}";
            var name = $"Get_GetConfig name:{positionOptions.Name}";
            Logger.LogWarning(title);
            Logger.LogWarning(name);
        }
    }

    //通过命令设置参数
    //set MyKey = "My key from Environment"
    //set Position__Title = Environment_Editor
    //set Position__Name = Environment_Rick
    //dotnet run
}
