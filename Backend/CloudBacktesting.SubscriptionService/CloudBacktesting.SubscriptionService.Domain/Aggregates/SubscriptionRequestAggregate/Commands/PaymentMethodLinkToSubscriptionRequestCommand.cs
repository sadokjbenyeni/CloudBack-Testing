using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class PaymentMethodLinkToSubscriptionRequestCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public PaymentAction SubscriptionRequestPaymentAction { get; set; }

        public PaymentMethodLinkToSubscriptionRequestCommand(SubscriptionRequestId subscriptionRequestId, PaymentAction subscriptionRequestPaymentAction) : base(subscriptionRequestId)
        {
            SubscriptionRequestPaymentAction = subscriptionRequestPaymentAction;
        }
    }
}
