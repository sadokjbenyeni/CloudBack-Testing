using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Client.SubscriptionRequest;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            var userId = this.User.Identity.Name;
            //// TODO: Do Query to get the User in Read Model SubscriptionAccountDto
            //return Task.FromResult((IActionResult)Ok(new SubscriptionAccountDto() { Email = userId }));
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<SubscriptionRequestReadModel>(model => true), CancellationToken.None);
            //await cursor.MoveNextAsync();
            return Ok(result.Select(ToDto).ToList());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //if (this.User != null && this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    _logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}

            //var readModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriptionRequestReadModel>(id), CancellationToken.None);
            //return Ok(readModel);
            var readModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriptionRequestReadModel>(new SubscriptionRequestId(id)), CancellationToken.None);
            return base.Ok(ToDto(readModel));
        }

        private static SubscriptionRequestReadModelDto ToDto(SubscriptionRequestReadModel readModel)
        {
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
    };
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSubscriptionRequestDto value)
        {
            var command = new SubscriptionRequestCreationCommand(new SubscriptionAccountId(value.SubscriptionAccountId).ToString(), value.Type);
            //if (this.User == null || !this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    _logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}
            //var notValidData = new List<string>();
            //if (string.IsNullOrEmpty(subscriptionRequestCommand.Type))
            //{
            //    notValidData.Add("Type of subscription cannot be null or empty");
            //}

            //if (notValidData.Any())
            //{
            //    return BadRequest(string.Join(Environment.NewLine, notValidData));
            //}

            //var requestId = SubscriptionRequestId.New;
            //var request = new SubscriptionRequest(requestId);
            //if (commandResult.IsSuccess)
            //{
            IExecutionResult commandResult = null;
            try
            {
                //var task = commandBus.PublishAsync(command, CancellationToken.None);
                //task.ConfigureAwait(true);
                //commandResult = await task;
                commandResult = await commandBus.PublishAsync(command, CancellationToken.None);
                if (commandResult.IsSuccess)
                {
                    return Ok(new IdentifierDto{ Id = command.AggregateId.Value });
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
            //}
            //var errorMessage = string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors);
            //_logger.LogError($"[Business, Error]Subscription failed for {User.Identity.Name}, type of command {commandDto.SubscriptionType}.{Environment.NewLine}{errorMessage}");
            //return BadRequest(errorMessage);
        }

        //[HttpPut]
        //public async Task<ActionResult> Put([FromBody] UpdateSubscriptionRequestDto value)
        //{
        //    var subscriptionRequestCommand = new SubscriptionRequestCreationCommand(value.SubscriptionAccountId, value.Subscriber, value.Type, value.Status);

        //    await commandBus.PublishAsync(subscriptionRequestCommand, CancellationToken.None);

        //    return CreatedAtAction(nameof(Get), new { id = subscriptionRequestCommand.AggregateId.Value }, subscriptionRequestCommand);
        //}
    }
}
