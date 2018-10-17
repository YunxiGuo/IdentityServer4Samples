using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var dico = DiscoveryClient.GetAsync("http://localhost:5100").GetAwaiter().GetResult();
            if (dico.IsError)
            {
                Console.WriteLine(dico.IsError);
                return;
            }
            var tockenClient = new TokenClient(dico.TokenEndpoint,"client","secret");
            var tockenResponse = tockenClient.RequestClientCredentialsAsync("api1").GetAwaiter().GetResult();
            if (tockenResponse.IsError)
            {
                Console.WriteLine(tockenResponse.Error);
                return;
            }
            Console.WriteLine(tockenResponse.AccessToken);
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tockenResponse.AccessToken);
            var response = httpClient.GetAsync("http://localhost:5200/api/values").GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine(JArray.Parse(content));
            }

            //MainAsync().Wait();
            Console.ReadKey();
        }
    }
}
