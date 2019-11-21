using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionAccountController : ControllerBase
    {
        private readonly ILogger<SubscriptionAccountController> _logger;
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public SubscriptionAccountController(ILogger<SubscriptionAccountController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
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

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<SubscriptionAccountReadModel>> Get(SubscriptionAccountId id)
        {
            //if (this.User != null && this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    _logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}
            var readModel = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriptionAccountReadModel>(id), CancellationToken.None);
            return Ok(readModel);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSubscriptionAccountDto value)
        {
            var subscriptionAccountCommand = new SubscriptionAccountCreationCommand(SubscriptionAccountId.New, value.Subscriber, value.SubscriptionDate);

            var subscriptionAccountId = SubscriptionAccountId.New;
            //if (this.User == null || !this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    _logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}
            //var commandResult = await subscriptionAccountManager.Ask<IExecutionResult>(command);
            //if (commandResult.IsSuccess)
            //{
            await _commandBus.PublishAsync(subscriptionAccountCommand, CancellationToken.None);
            return CreatedAtAction(nameof(Get), new { id = subscriptionAccountCommand.AggregateId.Value }, subscriptionAccountCommand);
            //}
            //logger.LogError($"[Business, Error]SubscriptionAccount for {command.SubscriptionUser} has not been created.");
            //return BadRequest();
        }
    }
}