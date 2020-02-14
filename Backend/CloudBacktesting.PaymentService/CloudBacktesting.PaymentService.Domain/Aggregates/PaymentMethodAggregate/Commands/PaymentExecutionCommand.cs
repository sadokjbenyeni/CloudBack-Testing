using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Sagas;
using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentExecutionCommand : Command<PaymentMethod, PaymentMethodId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string MethodId { get; set; }
        public string MerchantTransactionId { get; set; }
        public string SubsriptionRequestId { get; set; }
        public Card CardDetails { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }

        public PaymentExecutionCommand(PaymentMethodId paymentMethodId, string merchantTransactionId, string billingItemId, string subscriptionRequestId, Card cardDetails, string type, string subscriber) : base(paymentMethodId)
        {
            ItemId = billingItemId;
            MerchantTransactionId = merchantTransactionId;
            SubsriptionRequestId = subscriptionRequestId;
            CardDetails = cardDetails;
            Type = type;
            Subscriber = subscriber;
        }
    }
}
