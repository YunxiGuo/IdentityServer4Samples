using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ThirdLibrariyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dico = DiscoveryClient.GetAsync("http://localhost:5100").GetAwaiter().GetResult();
            if (dico.IsError)
            {
                Console.WriteLine(dico.IsError);
            }
            var tockenClient = new TokenClient(dico.TokenEndpoint,"client","secret");
            var tockenResponse = tockenClient.RequestClientCredentialsAsync("api1").GetAwaiter().GetResult();
            if (!tockenResponse.IsError)
            {
                Console.WriteLine(tockenResponse.AccessToken);
            }
            else
            {
                Console.WriteLine(tockenResponse.IsError);
            }
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tockenResponse.AccessToken);
            var response = httpClient.GetAsync("http://localhost:5200/api/values").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            //MainAsync().Wait();
            Console.ReadKey();
        }

        private static async Task MainAsync()
        {
            //
            var dico = await DiscoveryClient.GetAsync("http://localhost:5000");

            //token
            var tokenClient = new TokenClient(dico.TokenEndpoint, "client", "secret");
            var tokenresp = await tokenClient.RequestClientCredentialsAsync("api1");
            if (tokenresp.IsError)
            {
                Console.WriteLine(tokenresp.Error);
                return;

            }

            Console.WriteLine(tokenresp.Json);
            Console.WriteLine("\n\n");


            var client = new HttpClient();
            client.SetBearerToken(tokenresp.AccessToken);

            var resp = await client.GetAsync("http://localhost:5000/identity");
            if (!resp.IsSuccessStatusCode)
            {
                Console.WriteLine(resp.StatusCode);
            }
            else
            {
                var content = await resp.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }


        }
    }
}
