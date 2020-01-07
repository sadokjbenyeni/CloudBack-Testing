using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Repositories.BillingItemRepository;
using CloudBacktesting.PaymentService.Infra.Security.Claims;
using CloudBacktesting.PaymentService.WebAPI.Models;
using CloudBacktesting.PaymentService.WebAPI.Models.BillingItem;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingItemController : ControllerBase
    {
        private readonly ILogger<BillingItemController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public BillingItemController(ILogger<BillingItemController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
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
            var billingItemId = this.User.GetUserIdentifier().Value ?? "";
            if (string.IsNullOrEmpty(billingItemId))
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify (BillingItemId not). Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persists");
            }
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<BillingItemReadModel>(model => string.Equals(model.Id, billingItemId, StringComparison.InvariantCultureIgnoreCase)), CancellationToken.None);
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
            var readModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<BillingItemReadModel>(new BillingItemId(id)), CancellationToken.None);
            if (!string.Equals(readModel.Id, this.User.GetUserIdentifier()?.Value, StringComparison.InvariantCultureIgnoreCase))
            {
                return NotFound("This billing item is not found");
            }
            return base.Ok(ToDto(readModel));
        }

        private static BillingItemReadModelDto ToDto(BillingItemReadModel readModel)
        {
            if (readModel == null)
            {
                return null;
            }
            return new BillingItemReadModelDto()
            {

            };
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BillingItemDto value)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            var billingItemId = this.User.GetUserIdentifier()?.Value ?? "";
            if (string.IsNullOrEmpty(billingItemId))
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify (SubcriptionAccountId not found). Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persist");
            }
            IExecutionResult commandResult = null;
            try
            {
                var command = new BillingItemCreationCommand(new BillingItemId(billingItemId).ToString());
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
            logger.LogError($"[Business, Error] | '{errorIdentifier}' | BillingItem for {this.User?.Identity?.Name} has not been created.");
            logger.LogDebug($"[Business, Error, Message] | '{errorIdentifier}' | Error messages:{Environment.NewLine}{string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors)}");
            return BadRequest($"Creation of billing failed. Please contact support with error's identifier {errorIdentifier}");
        }
    }
}