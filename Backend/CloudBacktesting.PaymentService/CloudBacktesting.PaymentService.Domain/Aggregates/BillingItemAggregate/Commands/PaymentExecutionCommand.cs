using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class PaymentExecutionCommand : Command<BillingItem, BillingItemId>
    {
        public string MerchantTransactionId { get; set; }
        public string Subscriber { get; set; }
        public Card CardDetails { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }

        public PaymentExecutionCommand(BillingItemId billingItemId, string merchantTransactionId, string subscriber, Card cardDetails, double amount, string currency) : base(billingItemId)
        {
            MerchantTransactionId = merchantTransactionId;
            Subscriber = subscriber;
            CardDetails = cardDetails;
            Amount = amount;
            Currency = currency;
        }
    }
}
