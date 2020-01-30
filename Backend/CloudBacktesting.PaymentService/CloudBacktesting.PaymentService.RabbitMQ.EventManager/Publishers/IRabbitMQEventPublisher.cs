using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.RabbitMQ.EventManager.Publishers
{
    public interface IRabbitMQEventPublisher
    {
        bool PushMessage(object value, string exchange, string routingKey);

    }
}
