using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer
{
    public class Config
    {
        //scopes定义
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //给api资源定义一个scopes
                new ApiResource("api1","my api")
            };

        }

        //客户端注册，客户端能够访问的资源（通过：AllowedScopes）
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets=
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes={"api1"}
                }
            };
        }
    }
}