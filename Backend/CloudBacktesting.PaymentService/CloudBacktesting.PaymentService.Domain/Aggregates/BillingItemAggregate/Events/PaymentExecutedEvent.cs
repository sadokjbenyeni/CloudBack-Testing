using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class PaymentExecutedEvent : AggregateEvent<BillingItem, BillingItemId>
    {
        public string MerchantTransactionId { get; set; }
        public string BillingItemId { get; set; }
        public string Subscriber { get; set; }
        public Card CardDetails { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }

        public PaymentExecutedEvent(string merchantTransactionId, string billingItemId, string subscriber, Card cardDetails, double amount, string currency)
        {
            MerchantTransactionId = merchantTransactionId;
            BillingItemId = billingItemId;
            Subscriber = subscriber;
            CardDetails = cardDetails;
            Amount = amount;
            Currency = currency;
        }
    }
}
