using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class InvoiceGenerationCommand : Command<BillingItem, BillingItemId>
    {
        public string InvoiceId { get; }
        public string Client { get; }
        public string CardHolder { get; }

        public InvoiceGenerationCommand(string billingItemId, string invoiceId, string client, string cardHolder) : base(new BillingItemId(billingItemId))
        {
            InvoiceId = invoiceId;
            Client = client;
            CardHolder = cardHolder;

        }
    }
}
