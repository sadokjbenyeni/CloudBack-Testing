using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionRequestRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Exceptions;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Admin;
using CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Client.SubscriptionRequest;
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
    [CloudBacktestingAuthorize("Administrator")]
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
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<SubscriptionAccountReadModel>(model => true), CancellationToken.None);
            return Ok(result.Select(ToDto).ToList());
        }


        [HttpPut("validate")]
        public async Task<IActionResult> Validate([FromBody] ValidateSubscriptionRequestDto value)
        {
            var subscriptionRequestCommand = new SubscriptionRequestManualValidateSuccessCommand(new SubscriptionRequestId(value.SubscriptionId));
            var result = await commandBus.PublishAsync(subscriptionRequestCommand, CancellationToken.None);
            return result.IsSuccess ? Ok() : (IActionResult)BadRequest();
        }

        [HttpPut("decline")]
        public async Task<IActionResult> Decline([FromBody] DeclineSubscriptionRequestDto value)
        {
            var subscriptionRequestCommand = new SubscriptionRequestManualDeclineSuccessCommand(value.SubscriptionId, value.Message);
            var result = await commandBus.PublishAsync(subscriptionRequestCommand, CancellationToken.None);
            return result.IsSuccess ? Ok() : (IActionResult)BadRequest();
        }

        private SubscriptionAccountReadModelDto ToDto(SubscriptionAccountReadModel readModel)
        {
            return new SubscriptionAccountReadModelDto()
            {
                Id = readModel.Id,
                Subscriber = readModel.Subscriber,
                CreationDate = readModel.CreationDate
            };
        }
        private SubscriptionRequestReadModelDto ToDto(SubscriptionRequestReadModel readModel)
        {
            return new SubscriptionRequestReadModelDto()
            {
                Id = readModel.Id,
                SubscriptionAccountId = readModel.SubscriptionAccountId,
                Version = readModel.Version,
                Subscriber = readModel.Subscriber,
                Status = readModel.Status,
                Type = readModel.Type,
                OrderId = readModel.OrderId,
                CreationDate = readModel.CreationDate,
                IsSystemValidated = readModel.IsSystemValidated,
                IsManualValidated = readModel.IsManualValidated,
                DeclineMessage = readModel.DeclineMessage,
                ValidatedOrDeclinedDate = readModel.ValidatedOrDeclinedDate,
                RejectedDate = readModel.RejectedDate,
                IsManualConfigured = readModel.IsManualConfigured,
                ActivationMessage = readModel.ActivationMessage,
                ActivatedDate = readModel.ActivatedDate,
                PaymentMethodId = readModel.PaymentMethodId,
                PaymentAccountId = readModel.PaymentAccountId
            };
        }

        [HttpPut("configure")]
        public async Task<IActionResult> Configure([FromBody] ConfigureSubscriptionRequestDto value)
        {
            var subscriptionRequestCommand = new SubscriptionRequestManualConfigurationCommand(new SubscriptionRequestId(value.SubscriptionId), value.Message);
            var result = await commandBus.PublishAsync(subscriptionRequestCommand, CancellationToken.None);
            return result.IsSuccess ? Ok() : (IActionResult)BadRequest();
        }
        [HttpGet("SubscriptionRequests/{subscriptionState}")]
        public async Task<IActionResult> Get(SubscriptionState subscriptionState = SubscriptionState.All)
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }


            if (subscriptionState != SubscriptionState.All)
            {
                try{
                    var stringstatevalue = bindSubscriptionState(subscriptionState);
                    var readModel = await queryProcessor.ProcessAsync(new FindReadModelQuery<SubscriptionRequestReadModel>(item => item.Status == stringstatevalue), CancellationToken.None);
                    return base.Ok(readModel.ToList().Select(ToDto));
                }
                catch (NoBindingFoundException ex){
                    return base.BadRequest(ex.Message);
                }
            }
            else
            {
                var readModel = await queryProcessor.ProcessAsync(new FindReadModelQuery<SubscriptionRequestReadModel>(item => true), CancellationToken.None);
                return base.Ok(readModel.ToList().Select(ToDto));

            }
        }
        private string bindSubscriptionState(SubscriptionState value)

            => (value == SubscriptionState.PendingValidation) ? "Pending" : (value == SubscriptionState.PendingConfiguration) ? "Validated" : throw new NoBindingFoundException(value);
    }
}
