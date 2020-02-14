using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands;
using CloudBacktesting.PaymentService.RabbitMQ.EventManager.Models;
using CloudBacktesting.PaymentService.RabbitMQ.EventManager.Publishers;
using EventFlow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.RabbitMQ.EventManager.Consumers
{

    public class AccountCreatedListener : RabbitMQListener
    {
        private readonly ILogger<AccountCreatedListener> _logger;
        private readonly IRabbitMQAccountCreatedEventPublisher _rabbiteventproduce;
        private readonly IServiceProvider _services;
        private readonly ICommandBus _commandbus;
        public AccountCreatedListener(ICommandBus commandBus, IConnectionFactory factory, ILogger<AccountCreatedListener> logger, IServiceProvider services, IRabbitMQAccountCreatedEventPublisher rabbitEventProduce) : base(factory, logger)
        {
            _rabbiteventproduce = rabbitEventProduce;
            QueueName = "AccountActivationPayment";
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
                    var paymentAccount = JsonConvert.DeserializeObject<PaymentAccountRabbitMQDto>(message);
                    var exchange = "Signup";
                    var routingKey = "PaymentAccountCreated";
                    var command = new PaymentAccountCreationCommand(paymentAccount.User);
                    var commandResult = await _commandbus.PublishAsync(command, CancellationToken.None);
                    if (commandResult.IsSuccess)
                    {
                        paymentAccount.Id = command.AggregateId.Value;
                    }
                    Console.WriteLine($"message received {message}, sending user to exchange name : {exchange} with routing key : {routingKey}");
                    _rabbiteventproduce.PushMessage(paymentAccount, exchange, routingKey);
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