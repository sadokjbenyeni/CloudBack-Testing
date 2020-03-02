using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentMethodRepository;
using CloudBacktesting.PaymentService.Infra.Security.Claims;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentAccount;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentMethod;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Controllers.AdminPayment.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [CloudBacktestingAuthorize("Administrator")]
    public class AdminPaymentController : ControllerBase
    {
        private readonly ILogger<AdminPaymentController> logger;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public AdminPaymentController(ILogger<AdminPaymentController> logger, ICommandBus commandBus, IQueryProcessor queryProcessor)
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
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<PaymentAccountReadModel>(ModelBinderAttribute => true), CancellationToken.None);
            return Ok(result.Select(ToDtoAccount).ToList());
        }

        private static PaymentAccountReadModelDto ToDtoAccount(PaymentAccountReadModel readModel)
        {
            return new PaymentAccountReadModelDto()
            {
                Id = readModel.Id,
                Client = readModel.Client,
                CreationDate = readModel.CreationDate
            };
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
            var readModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PaymentMethodReadModel>(new PaymentMethodId(id)), CancellationToken.None);
            //var readModel = await queryProcessor.ProcessAsync(new FindReadModelQuery<PaymentMethodReadModel>(model => string.Equals(model.PaymentAccountId, paymentAccountId) && string.Equals(model.Id, id)), CancellationToken.None);
            if (IsNotFound(readModel))
            {
                return NoContent();
            }
            return base.Ok(ToDtoMethod(readModel));
        }
        private bool IsNotFound(PaymentMethodReadModel readModel)
        {
            return readModel == null
                || string.IsNullOrEmpty(readModel.Id);
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

    }
}