
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // TODO : Only administrator can access it
    public class AdminSubscriptionController : ControllerBase
    {
        private readonly ILogger<AdminSubscriptionController> logger;
        private readonly IQuerySubscriptionAccounts querySubscriptionAccount;

        public AdminSubscriptionController(ILogger<AdminSubscriptionController> logger, IQuerySubscriptionAccounts querySubscriptionAccount)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.querySubscriptionAccount = querySubscriptionAccount ?? throw new System.ArgumentNullException(nameof(querySubscriptionAccount));
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok(await querySubscriptionAccount.FindAll());
        //}

        //[HttpGet("{id:length(24)}", Name = "getSubscription")]
        //public async Task<IActionResult> Get(SubscriptionAccountId userIdentifier)
        //{
        //    return Ok(await querySubscriptionAccount.Find(userIdentifier));
        //}


    }
}
