using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.Infra.Security.Authorization;
using CloudBacktesting.PaymentService.Domain.Repositories.PaymentAccountRepository;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentAccount;
using EventFlow;
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
    [CloudBacktestingAuthorize("Admin")]
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
            if (this.User == null || this.User.Identity.IsAuthenticated)
            {
                var idError = Guid.NewGuid().ToString();
                logger.LogError($"[Security, Error] User not identify. Please check the API Gateway log. Id error: {idError}");
                return BadRequest($"Access error, please contact the administrator with error id: {idError}");
            }
            var result = await queryProcessor.ProcessAsync(new FindReadModelQuery<PaymentAccountReadModel>(ModelBinderAttribute => true), CancellationToken.None);
            return Ok(result.Select(ToDto).ToList());
        }

        private static PaymentAccountReadModelDto ToDto(PaymentAccountReadModel readModel)
        {
            return new PaymentAccountReadModelDto()
            {
                Id = readModel.Id,
                Client = readModel.Client,
                CreationDate = readModel.CreationDate
            };
        }
    }
}