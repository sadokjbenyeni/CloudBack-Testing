using CloudBacktesting.Infra.EventFlow.Queries;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
using CloudBacktesting.SubscriptionService.Domain.Repositories.SubscriptionAccountRepository;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
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
    // TODO : Only administrator can access it
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

     
    }
}
