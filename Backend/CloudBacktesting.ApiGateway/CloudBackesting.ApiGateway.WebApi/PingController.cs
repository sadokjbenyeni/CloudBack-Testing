using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CloudBackesting.ApiGateway.WebApi
{
    [ApiController]
    [Route("[controller]")]
    public class PingController:ControllerBase
    {   
        [HttpGet]
        public Task<IActionResult> Get()
        {
            return Task.FromResult((IActionResult) Ok());
        }
    }
}
