using Dotnet5WebAPI.Common;
using Dotnet5WebAPI.DA;
using Dotnet5WebAPI.Interface;
using Dotnet5WebAPI.Microsoft.EntityFrameworkCore;
using Dotnet5WebAPI.Models;
using Dotnet5WebAPI.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
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
using Newtonsoft.Json;
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

            //扩展方法,使用异常过滤器后不会用到中间件中异常处理
            services.AddControllers(config=> {
                config.Filters.Add(new MyExceptionFilter());
            
            }).AddNewtonsoftJson(options =>
            {
                //设置输出json格式
                // options.SerializerSettings.ContractResolver= new DefaultContractResolver();
            }); 

            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddDbContext<AuthCenterContext>(options=>
            {
                options.UseSqlServer(Configuration["AuthConnectionStrings:AuthContext"]);
            });
            services.AddScoped<ISystemUserRepository, SystemUserRepository>();
            services.AddScoped<ISystemUserService, SystemUserService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dotnet5WebAPI", Version = "v1" });
            });

            //可指定多个名称
            services.AddHttpClient("httpTestClient");
            services.AddHttpClient("MultipleParameter", httpClient =>
            {
                httpClient.BaseAddress = new Uri("http://portal.jc.yzw.cn.qa:8003");
                httpClient.DefaultRequestHeaders.Add("myHeader", "test");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogWarning(env.EnvironmentName);
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

            #region 异常处理
            //使用中间件处理系统异常，不推荐在此处处理，推荐在继承IExceptionFilter中处理
            //1.
            //app.UseExceptionHandler(configure =>
            //{
            //    //此处返回Json
            //    configure.Run(async context =>
            //    {
            //        var exceptionHandlerPathFeature =
            //           context.Features.Get<IExceptionHandlerPathFeature>();
            //        var content = new { exceptionHandlerPathFeature.Error.Message, Code = "500" };
            //        context.Response.ContentType = "application/json";
            //        context.Response.StatusCode = 500;
            //        await context.Response.WriteAsync(JsonConvert.SerializeObject(content));
            //    });
            //});
            //2.
            app.UseExceptionHandler("/error");
            #endregion




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
            app.MapWhen(httpContext =>
            {
                var isMatch = httpContext.Request.Query.ContainsKey("id");
                return isMatch;
            }, HandleMapWhenRunTest);

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
