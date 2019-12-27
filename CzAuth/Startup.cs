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
        /// �˷���������ʱ���á�ʹ�ô˷�����������ӵ�������
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRouting();
            //swagger api �ĵ�
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CzAuth API",
                    Description = "API for CzAuth",
                    Contact = new OpenApiContact() { Name = "��˼��", Email = "137238384@qq.com" }
                }); ;
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<UserContext>(options =>
                          options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// �˷���������ʱ���á�ʹ�ô˷�������HTTP����ܵ�
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

            //����swagger�м��
            app.UseSwagger(opt =>
            {
                //opt.RouteTemplate = "api/{controller=Home}/{action=Index}/{id?}";
            });
            //����SwaggerUI�м����htlm css js�ȣ�������swagger json ���
            app.UseSwaggerUI(s =>
            {

                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Webapi�ĵ�v1");
                s.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                s.ShowExtensions();
                s.InjectJavascript($"/Script/Swagger/swagger_lang.js");
                //Ҫ��Ӧ�õĸ� (http://localhost:<port>/) ���ṩ Swagger UI���뽫 RoutePrefix ��������Ϊ���ַ�����
                //s.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //nginx ������Ҫ�ض���Ͱ�ȫ������� 
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

        }
    }
}
