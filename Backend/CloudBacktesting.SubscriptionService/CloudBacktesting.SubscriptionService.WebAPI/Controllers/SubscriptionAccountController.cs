using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionAccountController : ControllerBase
    {
        private readonly ILogger<SubscriptionAccountController> logger;
        //private readonly ActorRefProvider<SubscriptionAccountManager> subscriptionAccountManager;

        public SubscriptionAccountController(ILogger<SubscriptionAccountController> logger /*ActorRefProvider<SubscriptionAccountManager> subscriptionAccountManager*/)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //this.subscriptionAccountManager = subscriptionAccountManager ?? throw new ArgumentNullException(nameof(subscriptionAccountManager));
        }

        [HttpGet]
        public IActionResult Get()
        {
            //if (this.User == null || !this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return Task.FromResult((IActionResult)BadRequest($"Access error, please contact the administrator with error id: {idError}"));
            //}
            //var userId = this.User.Identity.Name;
            //// TODO: Do Query to get the User in Read Model SubscriptionAccountDto
            //return Task.FromResult((IActionResult)Ok(new SubscriptionAccountDto() { Email = userId }));
            return Ok();
        }

        [HttpPost]
        public IActionResult Post()
        {
            //var subscriptionAccountId = SubscriptionAccountId.New;
            //if (this.User == null || !this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}
            //var command = new CreateSubscriptionAccountCommand(subscriptionAccountId, this.User.Identity.Name, DateTime.UtcNow);
            //var commandResult = await subscriptionAccountManager.Ask<IExecutionResult>(command);
            //if(commandResult.IsSuccess)
            //{
            return Ok();
            //}
            //logger.LogError($"[Business, Error]SubscriptionAccount for {command.SubscriptionUser} has not been created.");
            //return BadRequest();
        }
    }
}