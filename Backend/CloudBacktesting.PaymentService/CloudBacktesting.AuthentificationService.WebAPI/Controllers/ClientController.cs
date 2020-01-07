using System.Threading.Tasks;
using CloudBacktesting.AuthentificationService.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudBacktesting.AuthentificationService.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clients = await this.clientService.Get();
            return Ok(clients);
        }
    }
}