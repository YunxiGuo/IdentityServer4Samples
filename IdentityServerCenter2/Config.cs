using IdentityServer4.Models;
using System.Collections;
using System.Collections.Generic;

namespace IdentityServerCenter2
{
    public class Config
    {
        //定义要保护的资源（webapi）
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }
        //定义可以访问该API的客户端
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,  //设置模式，客户端模式
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}