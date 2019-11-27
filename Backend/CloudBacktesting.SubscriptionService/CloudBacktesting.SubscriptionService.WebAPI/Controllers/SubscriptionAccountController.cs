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

        public async Task<IActionResult> Get()
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<SubscriptionAccountReadModel>(model => true), CancellationToken.None);
            //await cursor.MoveNextAsync();
            return Ok(result.ToList());
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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSubscriptionAccountDto value)
        {
            var command = new SubscriptionAccountCreationCommand(value.Subscriber);
            //if (this.User == null || !this.User.Identity.IsAuthenticated)
            //{
            //    var idError = Guid.NewGuid().ToString();
            //    _logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
            //    return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            //}
            IExecutionResult commandResult = null;
            try
            {
                commandResult = await commandBus.PublishAsync(command, CancellationToken.None);
                if (commandResult.IsSuccess)
                {
                    return Ok(new { id = command.AggregateId.Value });
                }
            }
            catch(AggregateException aggregateEx)
            {
                commandResult = new FailedExecutionResult(new[] { aggregateEx.Message }.Union(aggregateEx.InnerExceptions.Select(ex => ex.Message)));
            }
            catch(Exception ex)
            {
                commandResult = new FailedExecutionResult(new[] { ex.Message });
            }
            var errorIdentifier = Guid.NewGuid().ToString();
            logger.LogError($"[Business, Error] | '{errorIdentifier}' | SubscriptionAccount for {command.Subscriber} has not been created.");
            logger.LogDebug($"[Business, Error, Message] | '{errorIdentifier}' | Error messages:{Environment.NewLine}{string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors)}");
            return BadRequest($"Creation of account for subscription failed. Please contact support with error's identifier {errorIdentifier}");
        }
    }
}