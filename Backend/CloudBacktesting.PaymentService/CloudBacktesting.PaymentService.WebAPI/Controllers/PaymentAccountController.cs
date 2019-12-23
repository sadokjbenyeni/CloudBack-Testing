using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentAccount;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.PaymentService.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentAccountController : ControllerBase
    {
        private readonly ILogger<PaymentAccountController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public PaymentAccountController(ILogger<PaymentAccountController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreatePaymentAccountDto value)
        {
            var command = new PaymentAccountCreationCommand(value.Client);
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            IExecutionResult commandResult = null;
            try
            {
                commandResult = await commandBus.PublishAsync(command, CancellationToken.None);
                if (commandResult.IsSuccess)
                {
                    return Ok(new { id = command.AggregateId.Value });
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
            logger.LogError($"[Business, Error] | '{errorIdentifier}' | PaymentAccount for {command.Client} has not been created.");
            logger.LogDebug($"[Business, Error, Message] | '{errorIdentifier}' | Error messages:{Environment.NewLine}{string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors)}");
            return BadRequest($"Creation of account for payment failed. Please contact support with error's identifier {errorIdentifier}");
        }
    }
}