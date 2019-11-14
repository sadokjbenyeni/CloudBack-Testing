using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akkatecture.Aggregates.ExecutionResults;
using Akkatecture.Akka;
using CloudBacktesting.SubscriptionService.Domain.Model;
using CloudBacktesting.SubscriptionService.Domain.Model.Commands;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionAccountController : ControllerBase
    {
        private readonly ILogger<SubscriptionAccountController> logger;
        private readonly ActorRefProvider<SubscriptionAccountManager> subscriptionAccountManager;

        public SubscriptionAccountController(ILogger<SubscriptionAccountController> logger, ActorRefProvider<SubscriptionAccountManager> subscriptionAccountManager)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.subscriptionAccountManager = subscriptionAccountManager ?? throw new ArgumentNullException(nameof(subscriptionAccountManager));
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return Task.FromResult((IActionResult)BadRequest($"Access error, please contact the administrator with error id: {idError}"));
            }
            var userId = this.User.Identity.Name;
            // TODO: Do Query to get the User in Read Model SubscriptionAccountDto
            return Task.FromResult((IActionResult)Ok(new SubscriptionAccountDto() { Email = userId }));
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            var command = new CreateSubscriptionAccountCommand() { UserIdentifier = this.User.Identity.Name };
            var commandResult = await subscriptionAccountManager.Ask<IExecutionResult>(command);
            if(commandResult.IsSuccess)
            {
                return Ok();
            }
            logger.LogError($"[Business, Error]SubscriptionAccount for {command.UserIdentifier} has not been created.");
            return BadRequest();
        }
    }
}