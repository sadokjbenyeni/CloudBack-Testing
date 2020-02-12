using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.Infra.Security.Claims;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Client.SubscriptionRequest;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [CloudBacktestingAuthorize("Client")]
    public class SubscriptionRequestController : ControllerBase
    {
        private readonly ILogger<SubscriptionRequestController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public SubscriptionRequestController(ILogger<SubscriptionRequestController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return Unauthorized($"Access error, please contact the administrator with error id: {idError}");
            }
            var subscriptionAccountId = this.User.GetUserIdentifier()?.Value ?? "";
            if (string.IsNullOrEmpty(subscriptionAccountId))
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify (SubcriptionAccountId not found). Please check the API Gateway log. Id error: {idError}");
                return Forbid($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persist");
            }
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<SubscriptionRequestReadModel>(model => string.Equals(model.SubscriptionAccountId, subscriptionAccountId)), CancellationToken.None);
            return Ok(result.Select(ToDto).ToList());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            var readModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriptionRequestReadModel>(new SubscriptionRequestId(id)), CancellationToken.None);
            if (!string.Equals(readModel.SubscriptionAccountId, this.User.GetUserIdentifier()?.Value, StringComparison.InvariantCultureIgnoreCase)) // FAIL => If the ID is not for subscription account defined
            {
                return NotFound("This subscription request is not found");
            }
            return base.Ok(ToDto(readModel));
        }

        private SubscriptionRequestReadModelDto ToDto(SubscriptionRequestReadModel readModel)
        {
            if (readModel == null)
            {
                return null;
            }
            return new SubscriptionRequestReadModelDto()
            {
                Id = readModel.Id,
                SubscriptionAccountId = readModel.SubscriptionAccountId,
                Status = readModel.Status,
                Subscriber = readModel.Subscriber,
                Type = readModel.Type,
                OrderId = readModel.OrderId,
                CreationDate = readModel.CreationDate,
                IsSystemValidated = readModel.IsSystemValidated,
                IsManualValidated = readModel.IsManualValidated,
                DeclineMessage = readModel.DeclineMessage,
                ValidatedOrDeclinedDate = readModel.ValidatedOrDeclinedDate,
                RejectedDate = readModel.RejectedDate,
                IsManualConfigured = readModel.IsManualConfigured,
                ActivatedDate = readModel.ActivatedDate,
                PaymentAccountId = readModel.PaymentAccountId,
                PaymentMethodId = readModel.PaymentMethodId,
                ActivationMessage = readModel.ActivationMessage
            };
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSubscriptionRequestDto value)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            var subscriptionAccountId = this.User.GetUserIdentifier()?.Value ?? "";
            var paymentMethodAccountId = this.User.GetUserPaymentIdentifier()?.Value ?? "";
            if (string.IsNullOrEmpty(subscriptionAccountId))
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify (SubcriptionAccountId not found). Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persist");
            }
            IExecutionResult commandResult = null;
            try
            {
                var command = new SubscriptionRequestCreationCommand(new SubscriptionAccountId(subscriptionAccountId).ToString(), value.Type, value.PaymentMethodId, paymentMethodAccountId);
                commandResult = await commandBus.PublishAsync(command, CancellationToken.None);
                if (commandResult.IsSuccess)
                {
                    return Ok(new IdentifierDto { Id = command.AggregateId.Value });
                }
            }
            catch (AggregateException aggregateEx)
            {
                commandResult = new FailedExecutionResult(new[] { aggregateEx.Message }.Union(aggregateEx.InnerExceptions.Select(ex => ex.Message)));
            }
            catch (Exception ex)
            {
                commandResult = new FailedExecutionResult(new[] { ex.Message });
            }
            var errorIdentifier = Guid.NewGuid().ToString();
            logger.LogError($"[Business, Error] | '{errorIdentifier}' | SubscriptionRequest for {this.User?.Identity?.Name} has not been created.");
            logger.LogDebug($"[Business, Error, Message] | '{errorIdentifier}' | Error messages:{Environment.NewLine}{string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors)}");
            return BadRequest($"Creation of subscription failed. Please contact support with error's identifier {errorIdentifier}");                                                            
        }

    }
}
