using CloudBacktesting.ApiGateway.WebApi.Ocelot.Models;
using System.Threading.Tasks;

namespace CloudBacktesting.ApiGateway.WebApi.Ocelot.Services
{
    public interface IUserService
    {
        Task<User> GetuserByTokenAsync(string Token);
    }
}