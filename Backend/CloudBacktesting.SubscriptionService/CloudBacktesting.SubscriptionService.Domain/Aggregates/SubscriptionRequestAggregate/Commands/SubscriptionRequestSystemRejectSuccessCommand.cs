using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestSystemRejectSuccessCommand : Command<SubscriptionRequest, SubscriptionRequestId>, ISubscriptionSagaRequestId
    {
        public string RequestId { get; set; }
        public string Subscriber { get; set; }
        public int OrderId { get; set; }

        public SubscriptionRequestSystemRejectSuccessCommand(string subscriptionId, string subscriber, int orderId) : base(new SubscriptionRequestId(subscriptionId))
        {
            RequestId = subscriptionId;
            Subscriber = subscriber;
            OrderId = orderId;
        }
    }
}
