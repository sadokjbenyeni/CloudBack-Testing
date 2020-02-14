using CloudBacktesting.PaymentService.Domain.Sagas;
using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodLinkToBillingItemCommand : Command<PaymentMethod, PaymentMethodId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string MerchantTransactionId { get; set; }
        public string Type { get; set; }
        public PaymentMethodLinkToBillingItemCommand(PaymentMethodId paymentMethodId, string itemId, string merchantTransactionId, string type) : base(paymentMethodId) 
        {
            MerchantTransactionId = merchantTransactionId;
            ItemId = itemId;
            Type = type;
        }
    }
}
