using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // TODO : Only administrator can access it
    public class AdminSubscriptionController : ControllerBase
    {
        public AdminSubscriptionController(ILogger<AdminSubscriptionController> logger)
        {

        }
    }
}
