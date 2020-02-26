using CloudBacktesting.PaymentService.Domain.Sagas;
using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class BillingItemLinkToPaymentMethodCommand : Command<PaymentMethod, PaymentMethodId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string Type { get; set; }
        public BillingItemLinkToPaymentMethodCommand(PaymentMethodId paymentMethodId, string itemId, string type) : base(paymentMethodId) 
        {
            ItemId = itemId;
            Type = type;
        }
    }
}
