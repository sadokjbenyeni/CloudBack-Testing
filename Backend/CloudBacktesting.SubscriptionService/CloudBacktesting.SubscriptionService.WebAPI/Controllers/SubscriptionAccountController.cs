using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.SubscriptionAccount;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.MongoDb.Driver.Extensions;
using CloudBacktesting.Infra.EventFlow.Queries;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //if (this.User != null && this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    _logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}
            var readModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriptionAccountReadModel>(new SubscriptionAccountId(id)), CancellationToken.None);
            return Ok(new SubscriptionAccountReadModelDto()
            {
                Id = readModel.Id,
                Subscriber = readModel.Subscriber,
            });
        }



        //[HttpPut]
        //public async Task<ActionResult> Put([FromBody] UpdateSubscriptionAccountDto value)
        //{
        //    var subscriptionAccountCommand = new SubscriptionAccountCreationCommand(value.Subscriber);

        //    await commandBus.PublishAsync(subscriptionAccountCommand, CancellationToken.None);

        //    return CreatedAtAction(nameof(Get), new { id = subscriptionAccountCommand.AggregateId.Value }, subscriptionAccountCommand);
        //}
    }
}