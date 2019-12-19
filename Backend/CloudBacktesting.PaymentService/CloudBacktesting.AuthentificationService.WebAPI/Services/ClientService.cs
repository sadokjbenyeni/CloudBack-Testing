using CloudBacktesting.AuthentificationService.WebAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.AuthentificationService.WebAPI.Services
{
    public interface IClientService
    {
        Task<Client> Authenticate(string token);
        Task<IEnumerable<Client>> Get();
    }

    public class ClientService : IClientService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Client> clients = new List<Client>
        {
            new Client { Id = "1", Token="blablabla" }
        };

        public async Task<Client> Authenticate(string token)
        {
            var client = await Task.Run(() => clients.SingleOrDefault(c => c.Token == token));

            if (client == null)
                return null;

            return client;
        }

        public async Task<IEnumerable<Client>> Get()
        {
            return await Task.Run(() => this.clients);
        }

    }
}
