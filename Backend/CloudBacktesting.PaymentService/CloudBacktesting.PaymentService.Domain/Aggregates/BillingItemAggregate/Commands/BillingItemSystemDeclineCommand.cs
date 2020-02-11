using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemSystemDeclineCommand : Command<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string PaymentMethodId { get; set; }

        public BillingItemSystemDeclineCommand(string itemId, string paymentMethodId) : base(new BillingItemId(itemId))
        {
            PaymentMethodId = paymentMethodId;
        }
    }
}
