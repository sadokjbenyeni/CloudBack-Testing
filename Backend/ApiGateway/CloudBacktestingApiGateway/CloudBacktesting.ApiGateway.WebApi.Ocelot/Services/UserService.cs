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
        public async Task<User> GetuserByTokenAsync(string token)
        {
            
            var client = new HttpClient();
            var semaabik = _consulClient.Catalog.Nodes().Result.Response;
            semaabik.ToList().ForEach(element =>
            {
                var a = element;
            });
            client.BaseAddress = new Uri("http://localhost:9095/api/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            var result = await client.GetAsync("user/info");
            if (result.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(await result.Content.ReadAsStringAsync());
                return user;
            }
            return null;

        }
    }
}
