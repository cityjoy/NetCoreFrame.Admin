using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreFrame.DataRepository;
using CoreFrame.Entity.Base_SysManage;
using CoreFrame.Util;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
namespace CoreFrame.Web
{
    public class Startup
    {
        public static ILoggerRepository Repository { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            //加载log4net日志配置文件
            Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));


        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(Configuration);
            services.AddLogging();
            services.AddAutoMapper();
            //使用Autofac替换自带IOC
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<BusinessModule>();
           
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            AutofacHelper.Container = container;
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/Error/Show");
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                //默认路由
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                //defaults: new { controller = "Home", action = "Index" }
                );

                //区域
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            InitEF();
        }

        private void InitEF()
        {
            Task.Run(() =>
            {
                try
                {
                    DbFactory.GetRepository(null, null, null).GetIQueryable<Base_User>().ToList();
                }
                catch
                {

                }
            });
        }
    }

    public class BusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<HomeBusiness>().AsImplementedInterfaces();
            //builder.RegisterType<Dev_ProjectBusiness>().As<IDev_ProjectBusiness>();
            #region 自动注入
            var assemblys = Assembly.Load("CoreFrame.Business");//获取程序集
            builder.RegisterAssemblyTypes(assemblys)
                  .Where(t => t.Name.EndsWith("Business"))//注册所有以"Business"结尾的类型
                  .AsImplementedInterfaces().InstancePerLifetimeScope(); 
            #endregion

        }
    }
}
