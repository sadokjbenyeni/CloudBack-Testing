using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class PaymentMethodLinkToSubscriptionRequestCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public string PaymentMethodId { get; set; }
        public string PaymentAccountId { get; set; }

        public PaymentMethodLinkToSubscriptionRequestCommand(SubscriptionRequestId subscriptionRequestId, string paymentMethodId, string paymentAcconutId) : base(subscriptionRequestId)
        {
            PaymentMethodId = paymentMethodId;
            PaymentAccountId = paymentAcconutId;
        }
    }
}
