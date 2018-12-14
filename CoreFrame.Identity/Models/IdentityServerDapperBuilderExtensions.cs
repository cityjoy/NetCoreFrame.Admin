using Dapper;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CoreFrame.IdentityServer.Models.Entity;
namespace CoreFrame.IdentityServer.Models
{
    /// <summary>
    /// 配置存储信息
    /// </summary>
    public class DapperStoreOptions
    {
        /// <summary>
        /// 是否启用自定清理Token
        /// </summary>
        public bool EnableTokenCleanup { get; set; } = false;

        /// <summary>
        /// 清理token周期（单位秒），默认1小时
        /// </summary>
        public int TokenCleanupInterval { get; set; } = 3600;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string DbConnectionStrings { get; set; }

    }


    /// <summary>
    /// 使用Dapper扩展IdentityServer存储
    /// </summary>
    public static class IdentityServerDapperBuilderExtensions
    {
        /// <summary>
        /// 配置Dapper接口和实现(默认使用SqlServer)
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="storeOptionsAction">存储配置信息</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddDapperSqlStore(
            this IIdentityServerBuilder builder,
            Action<DapperStoreOptions> storeOptionsAction = null)
        {
            var options = new DapperStoreOptions();
            builder.Services.AddSingleton(options);
            storeOptionsAction?.Invoke(options);
            builder.Services.AddTransient<IClientStore, SqlServerClientStore>();
            builder.Services.AddTransient<IResourceStore, SqlServerResourceStore>();
            builder.Services.AddTransient<IPersistedGrantStore, SqlServerPersistedGrantStore>();
            return builder;
        }

        /// <summary>
        /// 使用Mysql存储
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        //public static IIdentityServerBuilder AddDapperMySqlStore(this IIdentityServerBuilder builder)
        //{
        //    builder.Services.AddTransient<IClientStore, MySqlClientStore>();
        //    builder.Services.AddTransient<IResourceStore, MySqlResourceStore>();
        //    builder.Services.AddTransient<IPersistedGrantStore, MySqlPersistedGrantStore>();
        //    return builder;
        //}
    }

    /// <summary>
    /// 重写客户端存储信息
    /// </summary>
    public class SqlServerClientStore : IClientStore
    {
        private readonly ILogger<SqlServerClientStore> _logger;
        private readonly DapperStoreOptions _configurationStoreOptions;

        public SqlServerClientStore(ILogger<SqlServerClientStore> logger, DapperStoreOptions configurationStoreOptions)
        {
            _logger = logger;
            _configurationStoreOptions = configurationStoreOptions;
        }

        /// <summary>
        /// 根据客户端ID 获取客户端信息内容
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var cModel = new Client();
            var s = new Secret("myblogsecret999".Sha256());
            var _client = new Entity.Clients();
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                //由于后续未用到，暂不实现 ClientPostLogoutRedirectUris ClientClaims ClientIdPRestrictions ClientCorsOrigins ClientProperties,有需要的自行添加。
                string sql = @"select * from Clients where ClientId=@client and Enabled=1;
               select t2.* from Clients t1 inner join ClientGrantTypes t2 on t1.Id=t2.ClientId where t1.ClientId=@client and Enabled=1;
               select t2.* from Clients t1 inner join ClientRedirectUris t2 on t1.Id=t2.ClientId where t1.ClientId=@client and Enabled=1;
               select t2.* from Clients t1 inner join ClientScopes t2 on t1.Id=t2.ClientId where t1.ClientId=@client and Enabled=1;
               select t2.* from Clients t1 inner join ClientSecrets t2 on t1.Id=t2.ClientId where t1.ClientId=@client and Enabled=1;
                      ";
                var multi = await connection.QueryMultipleAsync(sql, new { client = clientId });
                var client = multi.Read<Client>();
                var ClientGrantTypes = multi.Read<Entity.ClientGrantTypes>();
                var ClientRedirectUris = multi.Read<Entity.ClientRedirectUris>();
                var ClientScopes = multi.Read<Entity.ClientScopes>();
                var ClientSecrets = multi.Read<Secret>();

                if (client != null && client.AsList().Count > 0)
                {//提取信息
                    cModel = client.AsList()[0];
                    cModel.AllowedGrantTypes = ClientGrantTypes.Select(m => m.GrantType).AsList();
                    cModel.RedirectUris = ClientRedirectUris.Select(m => m.RedirectUri).AsList();
                    cModel.AllowedScopes = ClientScopes.Select(m => m.Scope).AsList();
                    cModel.ClientSecrets = ClientSecrets.AsList();
                    //_client = client.AsList()[0];
                    //_client.AllowedGrantTypes = ClientGrantTypes.AsList();
                    //_client.RedirectUris = ClientRedirectUris.AsList();
                    //_client.AllowedScopes = ClientScopes.AsList();
                    //_client.ClientSecrets = ClientSecrets.AsList();
                    //cModel = _client.ToModel();
                }
            }
            _logger.LogDebug("{clientId} found in database: {clientIdFound}", clientId, _client != null);

            return cModel;
        }
    }

    /// <summary>
    /// 重写资源存储方法
    /// </summary>
    public class SqlServerResourceStore : IResourceStore
    {
        private readonly ILogger<SqlServerResourceStore> _logger;
        private readonly DapperStoreOptions _configurationStoreOptions;

        public SqlServerResourceStore(ILogger<SqlServerResourceStore> logger, DapperStoreOptions configurationStoreOptions)
        {
            _logger = logger;
            _configurationStoreOptions = configurationStoreOptions;
        }

        /// <summary>
        /// 根据api名称获取相关信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var model = new ApiResource();
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                string sql = @"select * from ApiResources where Name=@Name and Enabled=1;
                       select * from ApiResources t1 inner join ApiScopes t2 on t1.Id=t2.ApiResourceId where t1.Name=@name and Enabled=1;
                    ";
                var multi = await connection.QueryMultipleAsync(sql, new { name });
                var ApiResources = multi.Read<ApiResource>();
                var ApiScopes = multi.Read<Scope>();
                if (ApiResources != null && ApiResources.AsList()?.Count > 0)
                {
                    model = ApiResources.AsList()[0];
                    model.Scopes = ApiScopes.AsList();
                    if (model != null)
                    {
                        _logger.LogDebug("Found {api} API resource in database", name);
                    }
                    else
                    {
                        _logger.LogDebug("Did not find {api} API resource in database", name);
                    }
                    //model = apiresource.ToModel();
                }
            }
            return model;
        }

        /// <summary>
        /// 根据作用域信息获取接口资源
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var apiResourceData = new List<ApiResource>();
            string _scopes = "";
            foreach (var scope in scopeNames)
            {
                _scopes += "'" + scope + "',";
            }
            if (_scopes == "")
            {
                return null;
            }
            else
            {
                _scopes = _scopes.Substring(0, _scopes.Length - 1);
            }
            string sql = "select distinct t1.* from ApiResources t1 inner join ApiScopes t2 on t1.Id=t2.ApiResourceId where t2.Name in(" + _scopes + ") and Enabled=1;";
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                var apir = (await connection.QueryAsync<Entity.ApiResources>(sql))?.AsList();
                if (apir != null && apir.Count > 0)
                {
                    foreach (var apimodel in apir)
                    {
                        sql = "select * from ApiScopes where ApiResourceId=@id";
                        var scopedata = (await connection.QueryAsync<Scope>(sql, new { id = apimodel.Id }))?.AsList();
                        ApiResource identityApiResource = new ApiResource
                        {
                            Description = apimodel.Description,
                            DisplayName = apimodel.DisplayName,
                            Enabled = apimodel.Enabled,
                            Name = apimodel.Name,
                            Scopes = scopedata


                        };
                        apiResourceData.Add(identityApiResource);
                    }
                    _logger.LogDebug("Found {scopes} API scopes in database", apiResourceData.SelectMany(x => x.Scopes).Select(x => x.Name));
                }
            }
            return apiResourceData;
        }

        /// <summary>
        /// 根据scope获取身份资源
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var apiResourceData = new List<IdentityResource>();
            string _scopes = "";
            foreach (var scope in scopeNames)
            {
                _scopes += "'" + scope + "',";
            }
            if (_scopes == "")
            {
                return null;
            }
            else
            {
                _scopes = _scopes.Substring(0, _scopes.Length - 1);
            }
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                //暂不实现 IdentityClaims
                string sql = "select * from IdentityResources where Enabled=1 and Name in(" + _scopes + ")";
                var data = (await connection.QueryAsync<IdentityResource>(sql))?.AsList();
                if (data != null && data.Count > 0)
                {
                    foreach (var model in data)
                    {
                        apiResourceData.Add(model);
                    }
                }
            }
            return apiResourceData;
        }

        /// <summary>
        /// 获取所有资源实现
        /// </summary>
        /// <returns></returns>
        public async Task<Resources> GetAllResourcesAsync()
        {
            var apiResourceData = new List<ApiResource>();
            var identityResourceData = new List<IdentityResource>();
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                string sql = "select * from IdentityResources where Enabled=1";
                var data = (await connection.QueryAsync<IdentityResource>(sql))?.AsList();
                if (data != null && data.Count > 0)
                {

                    foreach (var m in data)
                    {
                        identityResourceData.Add(m);
                    }
                }
                //获取apiresource
                sql = "select * from ApiResources where Enabled=1";
                var apidata = (await connection.QueryAsync<Entity.ApiResources>(sql))?.AsList();
                if (apidata != null && apidata.Count > 0)
                {
                    foreach (var m in apidata)
                    {
                        sql = "select * from ApiScopes where ApiResourceId=@id";
                        var scopedata = (await connection.QueryAsync<Entity.ApiScopes>(sql, new { id = m.Id }))?.AsList();
                        m.Scopes = scopedata;
                        apiResourceData.Add(m.ToModel());
                    }
                }
            }
            var model = new Resources(identityResourceData, apiResourceData);
            return model;
        }
    }

    /// 重写授权信息存储
    /// </summary>
    public class SqlServerPersistedGrantStore : IPersistedGrantStore
    {
        private readonly ILogger<SqlServerPersistedGrantStore> _logger;
        private readonly DapperStoreOptions _configurationStoreOptions;

        public SqlServerPersistedGrantStore(ILogger<SqlServerPersistedGrantStore> logger, DapperStoreOptions configurationStoreOptions)
        {
            _logger = logger;
            _configurationStoreOptions = configurationStoreOptions;
        }

        /// <summary>
        /// 根据用户标识获取所有的授权信息
        /// </summary>
        /// <param name="subjectId">用户标识</param>
        /// <returns></returns>
        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                string sql = "select * from PersistedGrants where SubjectId=@subjectId";
                var data = (await connection.QueryAsync<Entity.PersistedGrants>(sql, new { subjectId }))?.AsList();
                var model = data.Select(x => x.ToModel());

                _logger.LogDebug("{persistedGrantCount} persisted grants found for {subjectId}", data.Count, subjectId);
                return model;
            }
        }

        /// <summary>
        /// 根据key获取授权信息
        /// </summary>
        /// <param name="key">认证信息</param>
        /// <returns></returns>
        public async Task<PersistedGrant> GetAsync(string key)
        {
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                string sql = "select * from PersistedGrants where [Key]=@key";
                var result = await connection.QueryFirstOrDefaultAsync<Entity.PersistedGrants>(sql, new { key });
                var model = result.ToModel();

                _logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", key, model != null);
                return model;
            }
        }

        /// <summary>
        /// 根据用户标识和客户端ID移除所有的授权信息
        /// </summary>
        /// <param name="subjectId">用户标识</param>
        /// <param name="clientId">客户端ID</param>
        /// <returns></returns>
        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                string sql = "delete from PersistedGrants where ClientId=@clientId and SubjectId=@subjectId";
                await connection.ExecuteAsync(sql, new { subjectId, clientId });
                _logger.LogDebug("remove {subjectId} {clientId} from database success", subjectId, clientId);
            }
        }

        /// <summary>
        /// 移除指定的标识、客户端、类型等授权信息
        /// </summary>
        /// <param name="subjectId">标识</param>
        /// <param name="clientId">客户端ID</param>
        /// <param name="type">授权类型</param>
        /// <returns></returns>
        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                string sql = "delete from PersistedGrants where ClientId=@clientId and SubjectId=@subjectId and Type=@type";
                await connection.ExecuteAsync(sql, new { subjectId, clientId });
                _logger.LogDebug("remove {subjectId} {clientId} {type} from database success", subjectId, clientId, type);
            }
        }

        /// <summary>
        /// 移除指定KEY的授权信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                string sql = "delete from PersistedGrants where [Key]=@key";
                await connection.ExecuteAsync(sql, new { key });
                _logger.LogDebug("remove {key} from database success", key);
            }
        }

        /// <summary>
        /// 存储授权信息
        /// </summary>
        /// <param name="grant">实体</param>
        /// <returns></returns>
        public async Task StoreAsync(PersistedGrant grant)
        {
            using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            {
                //移除防止重复
                await RemoveAsync(grant.Key);
                string sql = "insert into PersistedGrants([Key],ClientId,CreationTime,Data,Expiration,SubjectId,Type) values(@Key,@ClientId,@CreationTime,@Data,@Expiration,@SubjectId,@Type)";
                await connection.ExecuteAsync(sql, grant);
            }
        }
    }
}

