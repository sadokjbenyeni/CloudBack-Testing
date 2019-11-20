using System;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.Subscription;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ILogger<SubscriptionController> logger;
        
        public SubscriptionController(ILogger<SubscriptionController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            return Ok();            
        }
            

        [HttpGet("{id:length(24)}", Name = "getSubscription")]
        public IActionResult Get(SubscriptionId id)
        {
            //if (this.User != null && User.Identity.IsAuthenticated)
            //{
                return Ok();
            //}
            //var idError = Guid.NewGuid().ToString();
            //logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //return BadRequest($"Access error, please contact the administrator with error id: {idError}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            //if (this.User == null || !this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}
            //var notValidData = new List<string>();
            //if(string.IsNullOrEmpty(commandDto.SubscriptionType))
            //{
            //    notValidData.Add("Type of subscription cannot be null or empty");
            //}
            
            //if(notValidData.Any())
            //{
            //    return BadRequest(string.Join(Environment.NewLine, notValidData));
            //}
            //var accountId = SubscriptionAccountId.New;
            //var account = new SubscriptionAccountState();
            ////var accountId = await querySubscriptionAccount.Find((SubscriptionAccountId)User.Identity.Name);
            //var createCommand = new CreateSubscriptionAccountCommand(accountId,
            //                                                  account.SubscriptionUser,
            //                                                  account.SubscriptionDate);                                                             
            //var commandResult = await subscriptionAccountManager.Ask<IExecutionResult>(createCommand);            
            //if(commandResult.IsSuccess)
            //{
                return Ok();
            //}
            //var errorMessage = string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors);
            //logger.LogError($"[Business, Error]Subscription failed for {User.Identity.Name}, type of command {commandDto.SubscriptionType}.{Environment.NewLine}{errorMessage}");
            //return BadRequest(errorMessage);
        }

    }
}
