using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.RabbitMQ.EventManager.Publishers
{
    public class RabbitMQBillingItemCreatedEventPublisher : IRabbitMQEventPublisher
    {
        public readonly ILogger<RabbitMQBillingItemCreatedEventPublisher> _logger;
        public readonly IModel _channel;

        public RabbitMQBillingItemCreatedEventPublisher(IConnectionFactory factory, ILogger<RabbitMQBillingItemCreatedEventPublisher> logger)
        {
            try
            {
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
                _logger = logger;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error while attempting to connect to the channel for the followin reason: {ex.Message}");
            }
        }
        public bool PushMessage(object value, string exchange, string routingKey)
        {
            try
            {
                _logger.LogInformation($"PushMessage routing key: {routingKey}");
                var json = JsonConvert.SerializeObject(value);
                var body = Encoding.UTF8.GetBytes(json);
                _channel.BasicPublish(exchange: exchange, routingKey: routingKey, mandatory: true, basicProperties: null, body: body);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An error occured while publishing the message for the following reason {ex.Message}");
                return false;
            }
        }
    }
}
