using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate
{
    public class BillingItem : AggregateRoot<BillingItem, BillingItemId>, IEmit<BillingItemCreatedEvent>, IEmit<InvoiceGeneratedEvent>
    {
        private string paymentMethodId;

        public BillingItem(BillingItemId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string paymentMethodId)
        {
            Emit(new BillingItemCreatedEvent(paymentMethodId));
            return ExecutionResult.Success();
        }

        public IExecutionResult GenerateInvoice(string invoiceId, string method, string client, string cardHolder, string address, string amount, DateTime invoiceDate)
        {
            Emit(new InvoiceGeneratedEvent(invoiceId, method, client, cardHolder, address, amount, invoiceDate)) ;
            return ExecutionResult.Success();
        }
        public void Apply(BillingItemCreatedEvent aggregateEvent)
        {
            this.paymentMethodId = aggregateEvent.PaymentMethodId;
        }

        public void Apply(InvoiceGeneratedEvent aggregateEvent) { }
    }
}
