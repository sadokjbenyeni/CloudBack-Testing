using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class PaymentExecutedEvent : AggregateEvent<BillingItem, BillingItemId>
    {
        public string MerchantTransactionId { get; }
        public string BillingItemId { get; }
        public string PaymentMethodId { get; }
        public string Subscriber { get; }
        public Card CardDetails { get; }
        public double Amount { get; }
        public string Currency { get; }
        public bool IsPaymentSuccessful { get; }

        public PaymentExecutedEvent(string merchantTransactionId, string billingItemId,string paymentMethodId, string subscriber, Card cardDetails, double amount, string currency, bool isPaymentSuccessful)
        {
            MerchantTransactionId = merchantTransactionId;
            BillingItemId = billingItemId;
            PaymentMethodId = paymentMethodId;
            Subscriber = subscriber;
            CardDetails = cardDetails;
            Amount = amount;
            Currency = currency;
            IsPaymentSuccessful = isPaymentSuccessful;
        }
    }
}
