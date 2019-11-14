using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akkatecture.Aggregates.ExecutionResults;
using Akkatecture.Akka;
using CloudBacktesting.SubscriptionService.Domain.Model;
using CloudBacktesting.SubscriptionService.Domain.Model.Commands;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ILogger<SubscriptionController> logger;
        private readonly ActorRefProvider<SubscriptionAccountManager> subscriptionAccountManager;
        private readonly IQuerySubscription querySubscription;
        private readonly IQuerySubscriptionAccount querySubscriptionAccount;

        public SubscriptionController(ILogger<SubscriptionController> logger, 
            ActorRefProvider<SubscriptionAccountManager> subscriptionAccountManager, 
            IQuerySubscription querySubscription,
            IQuerySubscriptionAccount querySubscriptionAccount)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.subscriptionAccountManager = subscriptionAccountManager ?? throw new ArgumentNullException(nameof(subscriptionAccountManager));
            this.querySubscription = querySubscription ?? throw new ArgumentNullException(nameof(querySubscription));
            this.querySubscriptionAccount = querySubscriptionAccount ?? throw new ArgumentNullException(nameof(querySubscriptionAccount));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            return Ok(await querySubscription.FindByUserId(this.User.Identity.Name));            
        }
            

        [HttpGet("{id:length(24)}", Name = "getSubscription")]
        public ActionResult<SubscriptionAccountDto> Get(string id)
        {
            if (this.User != null && User.Identity.IsAuthenticated)
            {
                return Ok(querySubscription.Find(id, this.User.Identity.Name));
            }
            var idError = Guid.NewGuid().ToString();
            logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            return BadRequest($"Access error, please contact the administrator with error id: {idError}");
        }

        [HttpPost]
        public async IActionResult Post(CreateSubscriptionCommandDto commandDto)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            var notValidData = new List<string>();
            if(string.IsNullOrEmpty(commandDto.SubscriptionType))
            {
                notValidData.Add("Type of subscription cannot be null or empty");
            }
            
            if(notValidData.Any())
            {
                return BadRequest(string.Join(Environment.NewLine, notValidData));
            }
            var accountId = await querySubscriptionAccount.Find(User.Identity.Name);
            var createCommand = new CreateSubscriptionCommand(new SubscriptionAccountId(accountId.Id), 
                                                              new SubscriptionUser(accountId.UserIdentifier),
                                                              new SubscriptionType(commandDto.SubscriptionType),
                                                              new SubscriptionDate(DateTime.UtcNow));                                                              
            var commandResult = await subscriptionAccountManager.Ask<IExecutionResult>(createCommand);            
            if(commandResult.IsSuccess)
            {
                return Ok();
            }
            var errorMessage = string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors);
            logger.LogError($"[Business, Error]Subscription failed for {User.Identity.Name}, type of command {commandDto.SubscriptionType}.{Environment.NewLine}{errorMessage}");
            return BadRequest(errorMessage);
        }

    }
}
