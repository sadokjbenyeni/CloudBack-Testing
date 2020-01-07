using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Consumers
{
    public class RabbitMQListener : IHostedService
    {
        ILogger _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConnectionFactory _factory;
        protected string QueueName;
        public RabbitMQListener(IConnectionFactory factory, ILogger<RabbitMQListener> logger)
        {
            try
            {
                _factory = factory;
                _connection = _factory.CreateConnection();
                _channel = _connection.CreateModel();
                _logger = logger;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
                _logger.LogCritical($"rabbitMQ Listener error, Reason: {ex.Message}");
            }
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }

        private void   Register()
        {
            Console.WriteLine("starting");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                Console.WriteLine("message received");

                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                var result = await Process(message);
                if (result)
                {
                    _channel.BasicAck(ea.DeliveryTag, true);
                }
            };
            _channel.BasicConsume(queue: QueueName, consumer: consumer);
        }

        public virtual Task<bool> Process(string message)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("stopping");
            Deregister();
            return Task.CompletedTask;
        }
        private void Deregister()
        {
            Console.WriteLine("deregistering");
            _connection.Close();
        }
    }
}
