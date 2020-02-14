using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events
{
    public class InvoiceGeneratedEvent : AggregateEvent<BillingItem, BillingItemId>, IBillingSagaItemId
    {
        public string ItemId { get; set; }
        public string InvoiceId { get; }
        public DateTime InvoiceDate { get; }
        public string Client { get; }
        public string CardHolder { get; }

        public InvoiceGeneratedEvent(string billingItemId, string invoiceId, string client, string cardHolder, DateTime invoiceDate)
        {
            ItemId = billingItemId;
            InvoiceId = invoiceId;
            Client = client;
            CardHolder = cardHolder;
            InvoiceDate = invoiceDate;
        }
    }
}
