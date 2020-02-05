using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands;
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
    public class AccountCreatedListener : RabbitMQListener
    {
        private readonly ILogger<AccountCreatedListener> _logger;
        private readonly IRabbitMQEventPublisher _rabbiteventproduce;
        private readonly IServiceProvider _services;
        private readonly ICommandBus _commandbus;
        public AccountCreatedListener(ICommandBus commandBus, IConnectionFactory factory, ILogger<AccountCreatedListener> logger, IServiceProvider services, IRabbitMQEventPublisher rabbitEventProduce) : base(factory, logger)
        {
            _rabbiteventproduce = rabbitEventProduce;
            QueueName = "AccountActivationSubscription";
            _logger = logger;
            _services = services;
            _commandbus = commandBus;

        }
        public override async Task<bool> Process(string message)
        {
            Console.WriteLine("processing");
            try
            {
                using (var scope = _services.CreateScope())
                {
                    var subscriptionAccount= JsonConvert.DeserializeObject<SubscriptionAccountRabbitMQDto>(message);
                    var exchange = "Signup";
                    var routingKey = "SubscriptionCreated";
                    var command = new SubscriptionAccountCreationCommand(subscriptionAccount.User);
                    var commandResult = await _commandbus.PublishAsync(command, CancellationToken.None);
                    if (commandResult.IsSuccess)
                    {
                        subscriptionAccount.Id = command.AggregateId.Value;
                    }
                    Console.WriteLine($"message received {message}, sending user to exchange name : {exchange} with routing key : {routingKey}");
                    _rabbiteventproduce.PushMessage(subscriptionAccount, exchange, routingKey);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An error occured while publishing the message for the following reason {ex.Message}");
                return false;
            }

        }
    }
}

