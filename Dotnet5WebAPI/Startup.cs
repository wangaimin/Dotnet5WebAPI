using Dotnet5WebAPI.Interface;
using Dotnet5WebAPI.Models;
using Dotnet5WebAPI.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //使用扩展方法注册服务组
            services.AddConfig(Configuration);

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();

            //读取配置文件
            services.AddScoped<ConfigService>();

            //扩展方法
            services.AddControllers();
            services.AddDbContext<TodoContext>(opt=>opt.UseInMemoryDatabase("TodoList"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dotnet5WebAPI", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet5WebAPI v1"));
            }
            //可通过launchSettings.json设置、命令行设置、
            if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet5WebAPI v1"));
            }
            if (env.IsProduction())
            {

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //自定义中间件
            app.UseMyMiddleware();
            app.UseMyFirstMiddleware();
            app.UseMySecondMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //中间件分支,可选择执行Run直接结束或Use添加中间处理过程
            //url中含有api就会执行该中间件
            app.Map("/WeatherForecast", HandleMapRunTest);
            app.MapWhen(httpContext=>
            {
                var isMatch=httpContext.Request.Query.ContainsKey("id");
                return isMatch;
             } , HandleMapWhenRunTest);

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello, World!");
            //});
        }

        private static void HandleMapRunTest(IApplicationBuilder app)
        {
            //use还有后续流程可执行
            //run不会有后续流程，直接返回结果给用户
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test ");
            });
        }

        private static void HandleMapWhenRunTest(IApplicationBuilder app)
        {
            //use还有后续流程可执行
            //run不会有后续流程，直接返回结果给用户
            app.UseMiddleware<MapWhenMiddleware>();
        }
    }
}
