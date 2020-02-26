using CloudBacktesting.PaymentService.Domain.Sagas;
using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class PaymentExecutionInitializedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; }
        public string PaymentMethodId { get; }
        public string MerchantTransactionId { get; }
        public string SubscriptionType { get; }
        public string Subscriber { get; }
        public Card CreditCard { get; set; }

        public PaymentExecutionInitializedEvent(string itemId, string paymentMethodId, string merchantTransactionId, string subscriptionType, string subscriber, Card creditCard)
        {
            ItemId = itemId;
            MerchantTransactionId = merchantTransactionId;
            PaymentMethodId = paymentMethodId;
            SubscriptionType = subscriptionType;
            Subscriber = subscriber;
            CreditCard = creditCard;
        }
    }
}
