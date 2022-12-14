using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository;
using CloudBacktesting.PaymentService.Infra.Security.Claims;
using CloudBacktesting.PaymentService.WebAPI.Models;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentAccount;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudBacktesting.PaymentService.WebAPI.Controllers.PaymentAccount.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [CloudBacktestingAuthorize("Connected,Client")]
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

        private static PaymentAccountReadModelDto ToDto(PaymentAccountReadModel readModel)
        {
            if (readModel == null)
            {
                return null;
            }
            return new PaymentAccountReadModelDto()
            {
                Id = readModel.Id,
                Client = readModel.Client,
                CreationDate = readModel.CreationDate,
            };
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not authentificate. Please check the API Gateway log. Id error: {idError}");
                return Unauthorized($"You are not authorized to use this request, please contact the administrator with error id: {idError}, if the problem persists");
            }

            var paymentAccountId = this.User.GetUserIdentifier()?.Value ?? "";
            if (string.IsNullOrEmpty(paymentAccountId))
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not authentificatted. Please check the API Gateway log. Id error: {idError}");
                return Forbid($"You are not authorized to use this request, please contact the administator with error id: {idError}, if the problem persists");
            }
            var result = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PaymentAccountReadModel>(new PaymentAccountId(paymentAccountId)), CancellationToken.None);
            return Ok(ToDto(result));
        }

        [HttpPost]
        [CloudBacktestingAuthorize("Administrator")]
        public async Task<ActionResult> Post([FromBody] CreatePaymentAccountDto value)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return Unauthorized($"Access error, please contact the administrator with error id: {idError}");
            }
            var command = new PaymentAccountCreationCommand(value.Client);
            var commandResult = await commandBus.PublishAsync(command, CancellationToken.None);
            if (commandResult.IsSuccess)
            {
                return Ok(new IdentifierDto { Id = command.AggregateId.Value });
            }
            var errorIdentifier = Guid.NewGuid().ToString();
            logger.LogError($"[Business, Error] | '{errorIdentifier}' | PaymentAccount for {command.Client} has not been created.");
            logger.LogDebug($"[Business, Error, Message] | '{errorIdentifier}' | Error messages:{Environment.NewLine}{string.Join(Environment.NewLine, ((FailedExecutionResult)commandResult).Errors)}");
            return BadRequest($"Creation of account for payment failed. Please contact support with error's identifier {errorIdentifier}");
        }

    }
}