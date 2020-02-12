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
        private readonly IRabbitMQEventPublisher _rabbiteventproduce;
        private readonly IServiceProvider _services;
        private readonly ICommandBus _commandbus;

        public RabbitMQBillingItemCreatedEventListener(ICommandBus commandBus, IConnectionFactory factory, ILogger<RabbitMQBillingItemCreatedEventListener> logger, IServiceProvider services, IRabbitMQEventPublisher rabbitEventProduce) : base(factory, logger)
        {
            _rabbiteventproduce = rabbitEventProduce;
            QueueName = "SubscriptionRequestValidation";
            _logger = logger;
            _services = services;
            _commandbus = commandBus;
        }

        public override async Task<bool> Process(string message)
        {
            Console.WriteLine("Billing Creation Listening Processing ...");
            try
            {
                using (var scope = _services.CreateScope())
                {
                    var subscriptionRequest = JsonConvert.DeserializeObject<SubscriptionRequestRabbitMQDto>(message);
                    var exchange = "Validation";
                    var routingKey = "SubscriptionValidated";
                    var command = new SubscriptionRequestManualValidateSuccessCommand(new SubscriptionRequestId(subscriptionRequest.Id));
                    var commandResult = await _commandbus.PublishAsync(command, CancellationToken.None);
                    if (commandResult.IsSuccess)
                    {
                        subscriptionRequest.Id = command.AggregateId.Value;
                    }
                    Console.WriteLine($"message received {message}, sending user to exchange name : {exchange} with routing key : {routingKey}");
                    _rabbiteventproduce.PushMessage(subscriptionRequest, exchange, routingKey);
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
