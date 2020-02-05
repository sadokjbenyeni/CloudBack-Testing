using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class InvoiceGeneratedEvent : AggregateEvent<BillingItem, BillingItemId>
    {
        public string InvoiceId { get; }
        public DateTime InvoiceDate { get; }
        public string Method { get; }
        public string Client { get; }
        public string CardHolder { get; }
        public string Address { get; }
        public string Amount { get; }

        public InvoiceGeneratedEvent(string invoiceId, string method, string client, string cardHolder, string address, string amount, DateTime invoiceDate)
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
