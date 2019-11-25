using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Models.SubscriptionRequest;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionRequestController : ControllerBase
    {
        private readonly ILogger<SubscriptionRequestController> _logger;
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public SubscriptionRequestController(ILogger<SubscriptionRequestController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    if (this.User != null || !this.User.Identity.IsAuthenticated)
        //    {
        //        var idError = Guid.NewGuid().ToString();
        //        _logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
        //        return BadRequest($"Access error, please contact the administrator with error id: {idError}");
        //    }

        //    var readModel = await _queryProcessor.ProcessAsync(new InMemoryQuery<SusbcriptionRequestReadModel>(), CancellationToken.None);
        //    return Ok(readModel);
        //}

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<SubscriptionRequestReadModel>> Get(SubscriptionRequestId id)
        {
            //if (this.User != null && this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    _logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}

            var readModel = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriptionRequestReadModel>(id), CancellationToken.None);
            return Ok(readModel);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSubscriptionRequestDto value)
        {
            var subscriptionRequestCommand = new SubscriptionRequestCreationCommand(SubscriptionRequestId.New, value.Subscriber, value.Type, value.Status, value.SubscriptionDate);
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
            await _commandBus.PublishAsync(subscriptionRequestCommand, CancellationToken.None);
            return CreatedAtAction(nameof(Get), new { id = subscriptionRequestCommand.AggregateId.Value }, subscriptionRequestCommand);
            //}
            //var errorMessage = string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors);
            //_logger.LogError($"[Business, Error]Subscription failed for {User.Identity.Name}, type of command {commandDto.SubscriptionType}.{Environment.NewLine}{errorMessage}");
            //return BadRequest(errorMessage);
        }
    }
}
