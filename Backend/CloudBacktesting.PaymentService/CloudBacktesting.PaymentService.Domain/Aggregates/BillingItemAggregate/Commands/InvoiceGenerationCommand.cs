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
        public DateTime InvoiceDate { get; }
        public string Method { get; }
        public string Client { get; }
        public string CardHolder { get; }
        public string Address { get; }
        public string Amount { get; }

        public InvoiceGenerationCommand(string billingItemId, string invoiceId, string method, string client, string cardHolder, string address, string amount, DateTime invoiceDate) : base(new BillingItemId(billingItemId))
        {
            InvoiceId = invoiceId;
            Method = method;
            Client = client;
            CardHolder = cardHolder;
            Address = address;
            Amount = amount;
            InvoiceDate = invoiceDate;
        }
    }
}
