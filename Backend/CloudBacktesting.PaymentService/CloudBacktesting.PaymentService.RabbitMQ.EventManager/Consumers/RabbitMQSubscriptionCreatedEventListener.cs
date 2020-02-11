﻿using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands;
using CloudBacktesting.PaymentService.RabbitMQ.EventManager.Models;
using CloudBacktesting.PaymentService.RabbitMQ.EventManager.Publishers;
using EventFlow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.RabbitMQ.EventManager.Consumers
{
    public class RabbitMQSubscriptionCreatedEventListener : RabbitMQListener
    {
        private readonly ILogger<RabbitMQSubscriptionCreatedEventListener> _logger;
        private readonly IRabbitMQEventPublisher _rabbiteventproduce;
        private readonly IServiceProvider _services;
        private readonly ICommandBus _commandbus;
        public RabbitMQSubscriptionCreatedEventListener(ICommandBus commandBus, IConnectionFactory factory, ILogger<RabbitMQSubscriptionCreatedEventListener> logger, IServiceProvider services, IRabbitMQEventPublisher rabbitEventProduce) : base(factory, logger)
        {
            _rabbiteventproduce = rabbitEventProduce;
            QueueName = "BillingItemCreation";
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
                    var billingItem = JsonConvert.DeserializeObject<BillingItemRabbitMQDto>(message);
                    var exchange = "Billing";
                    var routingKey = "BillingItemCreated";
                    var command = new BillingItemCreationCommand(billingItem.PaymentMethodId, billingItem.SubscriptionRequestId);
                    var commandResult = await _commandbus.PublishAsync(command, CancellationToken.None);
                    if (commandResult.IsSuccess)
                    {
                        Console.WriteLine($"message received {message}, sending user to exchange name : {exchange} with routing key : {routingKey}");
                        _rabbiteventproduce.PushMessage(billingItem, exchange, routingKey);
                        return true;
                    }
                    return false;
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