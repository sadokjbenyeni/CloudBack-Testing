namespace CloudBacktesting.SubscriptionService.RabbitMQ.EventManager.Publishers
{
    public interface IRabbitMQEventPublisher
    {
         bool PushMessage(object value, string exchange, string routingKey);

    }
}
