using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentMethodRepository;
using CloudBacktesting.PaymentService.Infra.Security.Claims;
using CloudBacktesting.PaymentService.WebAPI.Models;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentMethod;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.PaymentService.WebAPI.Controllers.PaymentMethod.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [CloudBacktestingAuthorize("Connected,Client")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly ILogger<PaymentMethodController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public PaymentMethodController(ILogger<PaymentMethodController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
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
            var paymentAccountId = this.User.GetUserIdentifier()?.Value ?? "";
            if (string.IsNullOrEmpty(paymentAccountId))
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify (PaymentAccountId not found). Please check the API Gateway log. Id error: {idError}");
                return Forbid($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persist");
            }//string.Equals(model.PaymentAccountId, paymentAccountId, StringComparison.InvariantCultureIgnoreCase)
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<PaymentMethodReadModel>(model => string.Equals(model.PaymentAccountId, paymentAccountId)), CancellationToken.None);
            return Ok(result.Select(ToDtoMethod).ToList());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return Unauthorized($"Access error, please contact the administrator with error id: {idError}");
            }
            var paymentAccountId = this.User.GetUserIdentifier()?.Value ?? "";
            //var readModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PaymentMethodReadModel>(new PaymentMethodId(id)), CancellationToken.None);
            var readModel = await queryProcessor.ProcessAsync(new FindReadModelQuery<PaymentMethodReadModel>(model => string.Equals(model.PaymentAccountId, paymentAccountId) && string.Equals(model.Id, id)), CancellationToken.None);
            //var /*readModelFirstElement*/ = readModel.FirstOrDefault();
            if (!readModel.Any())
            {
                return NoContent();
            }
            return base.Ok(ToDtoMethod(readModel.FirstOrDefault()));
        }


        private static PaymentMethodReadModelDto ToDtoMethod(PaymentMethodReadModel readModel)
        {
            if (readModel == null)
            {
                return null;
            }
            return new PaymentMethodReadModelDto()
            {
                PaymentMethodId = readModel.Id,
                PaymentAccountId = readModel.PaymentAccountId, //Need to delete this
                Numbers = readModel.CardNumber.Substring(Math.Max(0, readModel.CardNumber.Length - 4)),
                Network = readModel.CardType,
                Holder = readModel.CardHolder,
                ExpirationDate = readModel.ExpirationMonth + "/" + readModel.ExpirationYear
            };
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCardPaymentMethodDto value)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return Unauthorized($"Access error, please contact the administrator with error id: {idError}");
            }
            var paymentAccountId = this.User.GetUserIdentifier()?.Value ?? "";
            if (string.IsNullOrEmpty(paymentAccountId))
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify (PaymentAccountId not found). Please check the API Gateway log. Id error: {idError}");
                return Forbid($"You are not authorize to use this request, please contact the administrator with error id: {idError}, if the problem persist");
            }
            IExecutionResult commandResult = null;
            try
            {
                var command = new PaymentMethodCreationCommand(new PaymentAccountId(paymentAccountId).Value, value.Numbers, value.Network, value.Holder, value.ExpirationYear, value.ExpirationMonth, value.Cryptogram);
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
            logger.LogError($"[Business, Error] | '{errorIdentifier}' | Payment for {this.User?.Identity?.Name} has not been created.");
            logger.LogDebug($"[Business, Error, Message] | '{errorIdentifier}' | Error messages:{Environment.NewLine}{string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors)}");
            return BadRequest($"Payment failed. Please contact support with error's identifier {errorIdentifier}");
        }

    }
}
