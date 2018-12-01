using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreFrame.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreFrame.FileStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(Configuration);
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public class BusinessModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                //builder.RegisterType<HomeBusiness>().AsImplementedInterfaces();
                //builder.RegisterType<Dev_ProjectBusiness>().As<IDev_ProjectBusiness>();
                #region 自动注入
                //var assemblys = Assembly.Load("CoreFrame.Business");//获取程序集
                //builder.RegisterAssemblyTypes(assemblys)
                //      .Where(t => t.Name.EndsWith("Business"))//注册所有以"Business"结尾的类型
                //      .AsImplementedInterfaces().InstancePerLifetimeScope();
                #endregion

            }
        }
    }
}
