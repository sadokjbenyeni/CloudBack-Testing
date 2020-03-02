using CloudBacktesting.Infra.EventFlow.Extensions;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.Infra.Security.Claims;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [CloudBacktestingAuthorize("Connected,Client")]
    public class SubscriptionAccountController : ControllerBase
    {
        private readonly ILogger<SubscriptionAccountController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public SubscriptionAccountController(ILogger<SubscriptionAccountController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        private static SubscriptionAccountReadModelDto ToDto(SubscriptionAccountReadModel readModel)
        {
            if (readModel == null)
            {
                return null;
            }
            return new SubscriptionAccountReadModelDto()
            {
                Id = readModel.Id,
                Subscriber = readModel.Subscriber,
                CreationDate = readModel.CreationDate,
            };
        }
        
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not authentificate. Please check the API Gateway log. Id error: {idError}");
                return Forbid($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persist");
            }
            var subscriptionAccountId = this.User.GetUserIdentifier()?.Value ?? "";
            if (string.IsNullOrEmpty(subscriptionAccountId))
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify (SubcriptionAccountId not found). Please check the API Gateway log. Id error: {idError}");
                return Unauthorized($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persist");
            }
            var result = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriptionAccountReadModel>(new SubscriptionAccountId(subscriptionAccountId)), CancellationToken.None);
            return Ok(ToDto(result));
        }

        [HttpPost]
        [CloudBacktestingAuthorize("Administrator")]
        public async Task<ActionResult> Post([FromBody] CreateSubscriptionAccountDto value)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not authentificate. Please check the API Gateway log. Id error: {idError}");
                return Forbid($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persist");
            }
            var command = new SubscriptionAccountCreationCommand(value.Subscriber);
            var commandResult = await commandBus.SafePublishAsync(command, CancellationToken.None);
            if (commandResult.IsSuccess)
            {
                return Ok(new IdentifierDto { Id = command.AggregateId.Value });
            }
            var errorIdentifier = Guid.NewGuid().ToString();
            logger.LogError($"[Business, Error] | '{errorIdentifier}' | SubscriptionAccount for {command.Subscriber} has not been created.");
            logger.LogDebug($"[Business, Error, Message] | '{errorIdentifier}' | Error messages:{Environment.NewLine}{string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors)}");
            return BadRequest($"Creation of account for subscription failed. Please contact support with error's identifier {errorIdentifier}");
        }
    }
}