using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.IdentityServer.Models
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("FileStoreApi","FileStoreApi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "myblogclient666",
                    RefreshTokenExpiration= TokenExpiration.Sliding,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,//授权方式，这里采用的是客户端认证模式，只要ClientId，以及ClientSecrets正确即可访问对应的AllowedScopes里面的api资源
                    ClientSecrets = { new Secret("myblogsecret999".Sha256()) },
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowedScopes = new List<string> {"FileStoreApi"}

                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
                new TestUser{
                    SubjectId = "1",
                    Username = "test",
                    Password = "1234"
                }
            };
        }
    }
}
