/********************************************************
 * Author:sihang,chen
 * CreateDate:2019/12/17
 * Version:0.1
 * *******************************************************/

using CzAuth.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;

namespace CzAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 此方法由运行时调用。使用此方法将服务添加到容器。
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRouting();
            //swagger api 文档
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CzAuth API",
                    Description = "API for CzAuth",
                    Contact = new OpenApiContact() { Name = "陈思行", Email = "137238384@qq.com" }
                }); ;
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<UserContext>(options =>
                          options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// 此方法由运行时调用。使用此方法配置HTTP请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //启用swagger中间件
            app.UseSwagger(opt =>
            {
                //opt.RouteTemplate = "api/{controller=Home}/{action=Index}/{id?}";
            });
            //启用SwaggerUI中间件（htlm css js等），定义swagger json 入口
            app.UseSwaggerUI(s =>
            {

                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Webapi文档v1");
                s.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                s.ShowExtensions();
                s.InjectJavascript($"/Script/Swagger/swagger_lang.js");
                //要在应用的根 (http://localhost:<port>/) 处提供 Swagger UI，请将 RoutePrefix 属性设置为空字符串：
                //s.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //nginx 配置需要重定向和安全策略相关 
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

        }
    }
}
