using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using AutoMapper;
using CommonUtil;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QiMen.Api.Filters;
using QiMen.Common;
using Swashbuckle.AspNetCore.Swagger;
using EasyCaching.InMemory;
using EasyCaching.Core;

namespace QiMen.Api
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置注册服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvc(options =>
            {
                //添加异常过滤捕捉
                options.Filters.Add<ExceptionFilter>();
                //添加操作进行前后过滤
                options.Filters.Add<ActionFilter>();

            });

            //配置Json输出格式
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = Constants.DATE_TIME_FORMAT;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            }
            );

            //配置Swagger生成服务
            services.AddSwaggerGen(c =>
                {
                    //配置Swagger头部认证说明
                    c.SwaggerDoc(Constants.SWAGGER_NAME, new Info()
                    {
                        Version = Constants.SWAGGER_VERSION,
                        Title = Constants.SWAGGER_TITLE,
                    });


                    //配置Swagger读取的xml文件注释文档
                    var file = Directory.EnumerateFiles(AppContext.BaseDirectory, Constants.XML_SEARCH_PATTERN).GetEnumerator();

                    while (file.MoveNext())
                    {
                        c.IncludeXmlComments(file.Current, true);
                    }
                }
            );

            //注册业务服务
            var bizServiceDictionary = AssemblyLoad(Constants.SERVICE_LIBRARY_NAME);
            var bizServiceEnumerator = bizServiceDictionary.GetEnumerator();
            while (bizServiceEnumerator.MoveNext())
            {
                var biz = bizServiceEnumerator.Current;

                services.AddTransient(biz.Value, biz.Key);
            }

            //注册AutoMapper映射服务
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddEasyCaching(option =>
            {
                // use memory cache with a simple way
                option.UseInMemory("default");
            });
        }

        /// <summary>
        /// 配置方法
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境下，异常进行捕捉
                app.UseExceptionHandler(options =>
                {
                    options.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = TopConstants.CONTENT_TYPE_TEXT_HTML;
                        var ex = context.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            string err = $"<h1>HttpStatusCode：{context.Response.StatusCode}</h1><h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
                            await context.Response.WriteAsync(err).ConfigureAwait(false);
                        }
                    });
                });
            }

            //启用Swagger
            app.UseSwagger();

            //启用SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Constants.SWAGGER_JSON_URL, Constants.API_LIBRARY_NAME);
            }
            );
            app.UseMvc();
        }

        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        private Dictionary<Type, Type> AssemblyLoad(string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            IEnumerable<Type> typeEnumerable = assembly.ExportedTypes;

            //排除基类服务
            IEnumerator<Type> typeEnumerator = typeEnumerable.Where(j => j.IsPublic && j.IsClass && !j.IsAbstract).GetEnumerator();

            Dictionary<Type, Type> result = new Dictionary<Type, Type>();

            while (typeEnumerator.MoveNext())
            {
                Type implementType = typeEnumerator.Current;

                Type interfaceType = implementType.GetInterfaces().First(j => j.Namespace == Constants.ISERVICE_LIBRARY_NAME);

                if (!result.ContainsKey(implementType))
                {
                    result.Add(implementType, interfaceType);
                }
            }

            return result;
        }
    }
}
