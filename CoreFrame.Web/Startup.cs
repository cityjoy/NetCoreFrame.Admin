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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//���ӻ��������ļ����½���ĿĬ����
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            //����log4net��־�����ļ�
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
            //ʹ��Autofac�滻�Դ�IOC
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
                //Ĭ��·��
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                //defaults: new { controller = "Home", action = "Index" }
                );

                //����
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
            #region �Զ�ע��
            var assemblys = Assembly.Load("CoreFrame.Business");//��ȡ����
            builder.RegisterAssemblyTypes(assemblys)
                  .Where(t => t.Name.EndsWith("Business"))//ע��������"Business"��β������
                  .AsImplementedInterfaces().InstancePerLifetimeScope(); 
            #endregion

        }
    }
}
