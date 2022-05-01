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

            //ʹ����չ����ע�������
            services.AddConfig(Configuration);

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();

            //��ȡ�����ļ�
            services.AddScoped<ConfigService>();

            //��չ����,ʹ���쳣�������󲻻��õ��м�����쳣����
            services.AddControllers(config=> {
                config.Filters.Add(new MyExceptionFilter());
            
            }).AddNewtonsoftJson(options =>
            {
                //�������json��ʽ
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

            //��ָ���������
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
            //��ͨ��launchSettings.json���á����������á�
            if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet5WebAPI v1"));
            }
            if (env.IsProduction())
            {

            }

            #region �쳣����
            //ʹ���м������ϵͳ�쳣�����Ƽ��ڴ˴������Ƽ��ڼ̳�IExceptionFilter�д���
            //1.
            //app.UseExceptionHandler(configure =>
            //{
            //    //�˴�����Json
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

            //�Զ����м��
            app.UseMyMiddleware();
            app.UseMyFirstMiddleware();
            app.UseMySecondMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //�м����֧,��ѡ��ִ��Runֱ�ӽ�����Use����м䴦�����
            //url�к���api�ͻ�ִ�и��м��
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
            //use���к������̿�ִ��
            //run�����к������̣�ֱ�ӷ��ؽ�����û�
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test ");
            });
        }

        private static void HandleMapWhenRunTest(IApplicationBuilder app)
        {
            //use���к������̿�ִ��
            //run�����к������̣�ֱ�ӷ��ؽ�����û�
            app.UseMiddleware<MapWhenMiddleware>();
        }
    }
}
