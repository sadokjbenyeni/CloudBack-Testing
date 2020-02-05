using CloudBacktesting.ApiGateway.WebApi.Ocelot.Models;
using Consul;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Services
{
    public class UserService : IUserService

    {
        IConsulClient _consulClient;
        public UserService(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }
        public async Task<UserReceivedData> GetuserByTokenAsync(string token)
        {
            var services = _consulClient.Catalog.Service("msservice").Result.Response;
            if (services.Any())
            {
                var msservice = services.ToList().First();
                var basadressmsservice = $"http://{msservice.ServiceAddress}:{msservice.ServicePort}";
                var client = new HttpClient();
                client.BaseAddress = new Uri(basadressmsservice);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                var result = await client.GetAsync("api/v1/user/info");
                if (result.IsSuccessStatusCode)
                {
                    var user = JsonConvert.DeserializeObject<UserReceivedData>(await result.Content.ReadAsStringAsync());
                    return user;
                }
            }
            return null;

        }
    }
}
