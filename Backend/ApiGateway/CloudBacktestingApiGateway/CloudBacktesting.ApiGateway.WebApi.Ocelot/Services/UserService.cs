using CloudBacktesting.ApiGateway.WebApi.Ocelot.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Services
{
    public class UserService : IUserService

    {
        public async Task<User> GetuserByTokenAsync(string token)
        {
            var client = new HttpClient();
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
