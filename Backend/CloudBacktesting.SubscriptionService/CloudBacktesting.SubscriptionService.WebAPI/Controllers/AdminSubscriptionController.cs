using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Admin;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // TODO : Only administrator can access it
    public class AdminSubscriptionController : ControllerBase
    {
        private readonly ILogger<AdminSubscriptionController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public AdminSubscriptionController(ILogger<AdminSubscriptionController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
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
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<SubscriptionAccountReadModel>(model => true), CancellationToken.None);
            //await cursor.MoveNextAsync();
            return Ok(result.ToList());
        }

        [HttpPut("validate")]
        public async Task<IActionResult> Validate([FromBody] ValidateSubscriptionRequestDto value)
        {
            var subscriptionRequestCommand = new SubscriptionRequestManualValidateSuccessCommand(new SubscriptionRequestId(value.Id));

            var result = await commandBus.PublishAsync(subscriptionRequestCommand, CancellationToken.None);
            return result.IsSuccess ? Ok() : (IActionResult)BadRequest();
            //return CreatedAtAction(nameof(Get), new IdentifierDto { Id = subscriptionRequestCommand.AggregateId.Value }, subscriptionRequestCommand);
        }

        [HttpPut("decline")]
        public async Task<IActionResult> Decline([FromBody] DeclineSubscriptionRequestDto value)
        {
            var subscriptionRequestCommand = new SubscriptionRequestManualDeclineSuccessCommand(value.Id, value.Message);

            var result = await commandBus.PublishAsync(subscriptionRequestCommand, CancellationToken.None);
            return result.IsSuccess ? Ok() : (IActionResult)BadRequest();
            //return CreatedAtAction(nameof(Get), new IdentifierDto { Id = subscriptionRequestCommand.AggregateId.Value, message = subscriptionRequestCommand.DeclineMessage}, subscriptionRequestCommand);
        }
    }
}
