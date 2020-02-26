using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Sagas;
using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class PaymentExecutionCommand : Command<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string MethodId { get; set; }
        public string MerchantTransactionId { get; set; }
        public string Type { get; set; }
        public string Subscriber { get; set; }
        public Card CardDetails { get; set; }

        public PaymentExecutionCommand(string paymentMethodId, string merchantTransactionId, BillingItemId billingItemId, string type, string subscriber, Card cardDetails) : base(billingItemId)
        {
            MethodId = paymentMethodId;
            MerchantTransactionId = merchantTransactionId;
            Type = type;
            Subscriber = subscriber;
            CardDetails = cardDetails;
        }
    }
}
