using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands;
using CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Models;
using CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Publishers;
using EventFlow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Consumers
{
    public class RabbitMQBillingItemCreatedEventListener : RabbitMQListener
    {
        private readonly ILogger<RabbitMQBillingItemCreatedEventListener> _logger;
        private readonly IServiceProvider _services;
        private readonly ICommandBus _commandbus;

        public RabbitMQBillingItemCreatedEventListener(ICommandBus commandBus, IConnectionFactory factory, ILogger<RabbitMQBillingItemCreatedEventListener> logger, IServiceProvider services) : base(factory, logger)
        {
            QueueName = "SubscriptionRequestCreation";
            _logger = logger;
            _services = services;
            _commandbus = commandBus;
        }

        public override async Task<bool> Process(string message)
        {
            Console.WriteLine("Subscription Creation Listening Processing ...");
            try
            {
                using (var scope = _services.CreateScope())
                {
                    var subscriptionRequest = JsonConvert.DeserializeObject<SubscriptionRequestRabbitMQDto>(message);
                    var exchange = "Billing";
                    var routingKey = "SubscriptionRequestCreated";
                    var command = new SubscriptionRequestManualValidateSuccessCommand(new SubscriptionRequestId(subscriptionRequest.SubscriptionRequestId));
                    var commandResult = await _commandbus.PublishAsync(command, CancellationToken.None);

                    Console.WriteLine($"message received {message}, sending user to exchange name : {exchange} with routing key : {routingKey}");
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"An error occured while publishing the message for the following reason {ex.Message}");
                return false;
            }
        }
    }
}
