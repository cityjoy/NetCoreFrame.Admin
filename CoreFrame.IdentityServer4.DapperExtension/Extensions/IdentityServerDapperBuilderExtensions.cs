using IdentityServer4.DapperExtension.Options;
using System;
using IdentityServer4.Stores;
using IdentityServer4.DapperExtension.Stores.SqlServer;
using IdentityServer4.DapperExtension.Interfaces;
using IdentityServer4.DapperExtension.HostedServices;
using Microsoft.Extensions.Hosting;
using IdentityServer4.DapperExtension.Stores.MySql;
using IdentityServer4.Services;
using IdentityServer4.Stores.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// dapper扩展
    /// 2018-12-03
    /// 使用Entity
    /// </summary>
    public static class IdentityServerDapperBuilderExtensions
    {
        /// <summary>
        /// 配置Dapper接口和实现(默认使用SqlServer)
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="storeOptionsAction">存储配置信息</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddDapperStore(
            this IIdentityServerBuilder builder,
            Action<DapperStoreOptions> storeOptionsAction = null)
        {
            var options = new DapperStoreOptions();
            builder.Services.AddSingleton(options);
            storeOptionsAction?.Invoke(options);
            builder.Services.AddTransient<IClientStore, SqlServerClientStore>();
            builder.Services.AddTransient<IResourceStore, SqlServerResourceStore>();
            builder.Services.AddTransient<IPersistedGrantStore, SqlServerPersistedGrantStore>();
            builder.Services.AddTransient<IPersistedGrants, SqlServerPersistedGrants>();
            //builder.Services.AddTransient<IHandleGenerationService, DefaultHandleGenerationService>();
            //builder.Services.AddTransient<IPersistentGrantSerializer, PersistentGrantSerializer>();
            //builder.Services.AddTransient<IReferenceTokenStore, DefaultReferenceTokenStore>();
            builder.Services.AddSingleton<TokenCleanup>();
            builder.Services.AddSingleton<IHostedService, TokenCleanupHost>();
            return builder;
        }

        /// <summary>
        /// 使用Mysql存储
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder UseMySql(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IClientStore, MySqlClientStore>();
            builder.Services.AddTransient<IResourceStore, MySqlResourceStore>();
            builder.Services.AddTransient<IPersistedGrantStore, MySqlPersistedGrantStore>();
            builder.Services.AddTransient<IPersistedGrants, MySqlPersistedGrants>();
            return builder;
        }
    }
}
