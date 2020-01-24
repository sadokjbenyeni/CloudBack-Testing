using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate
{
    public class BillingItem : AggregateRoot<BillingItem, BillingItemId>, IEmit<BillingItemCreatedEvent>, IEmit<BillingItemLinkedEvent>, IEmit<InvoiceGeneratedEvent>
    {
        private string paymentMethodId;

        public BillingItem(BillingItemId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string paymentMethodId)
        {
            Emit(new BillingItemCreatedEvent(paymentMethodId));
            return ExecutionResult.Success();
        }

        public IExecutionResult LinkBillingItem(string paymentMethodId)
        {
            Emit(new BillingItemLinkedEvent(paymentMethodId));
            return ExecutionResult.Success();
        }

        public IExecutionResult GenerateInvoice(string invoiceId, string method, string client, string cardHolder, string address, string amount, DateTime invoiceDate)
        {
            Emit(new InvoiceGeneratedEvent(invoiceId, method, client, cardHolder, address, amount, invoiceDate)) ;
            return ExecutionResult.Success();
        }
        public void Apply(BillingItemCreatedEvent @event)
        {
            this.paymentMethodId = @event.PaymentMethodId;
        }

        public void Apply(BillingItemLinkedEvent @event) { }

        public void Apply(InvoiceGeneratedEvent @event) { }
    }
}
