using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Publishers
{
    public class RabbitMQEventPublisher : IRabbitMQEventPublisher
    {
        public readonly ILogger<RabbitMQEventPublisher> _logger;
        public readonly IModel _channel;

        public RabbitMQEventPublisher(IConnectionFactory factory, ILogger<RabbitMQEventPublisher> logger)
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
        public virtual bool PushMessage(object value, string exchange, string routingKey)
        {
            try
            {
                _logger.LogInformation($"Push Message routing key: {routingKey}");
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
