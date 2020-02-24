using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Infra.Models;
using CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate
{
    public class BillingItem : AggregateRoot<BillingItem, BillingItemId>, IEmit<BillingItemCreatedEvent>
    {
        private string billingItemId;
        private string subscriptionType;
        private string merchantTransactionId;
        private string paymentMethodId;
        private string subscriptionRequestId;
        private string status;

        public BillingItem(BillingItemId aggregateId) : base(aggregateId) { }

        public IExecutionResult Create(string paymentMethodId, string paymentMethodStatus, string subscriptionRequestId, string subscriptionType)
        {
            Emit(new BillingItemCreatedEvent(this.Id.Value, paymentMethodId, subscriptionRequestId, "Creating", paymentMethodStatus, DateTime.UtcNow, subscriptionType));
            return ExecutionResult.Success();
        }

        public IExecutionResult LinkSubscriptionNPaymentToBilling(string subscriptionRequestId, string paymentMethodId, string paymentMethodStatus, string subscriptionType)
        {
            Emit(new SubscriptionNPaymentToBillingLinkedEvent(this.Id.Value, subscriptionRequestId, paymentMethodId, paymentMethodStatus, subscriptionType));
            return ExecutionResult.Success();
        }

        public IExecutionResult GenerateInvoice(string invoiceId, string client, string cardHolder)
        {
            Emit(new InvoiceGeneratedEvent(this.Id.Value, invoiceId, client, cardHolder, DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public IExecutionResult InitializePayment()
        {
            Emit(new PaymentExecutionInitializedEvent(this.Id.Value, this.paymentMethodId, new MerchantTransaction().Id, this.subscriptionType));
            return ExecutionResult.Success();

        }

        public IExecutionResult DeclineBySystem(string paymentMethodId)
        {
            Emit(new BillingItemStatusUpdatedEvent("Declined", this.Id.Value));
            Emit(new BillingItemSystemDeclinedEvent(this.Id.Value, paymentMethodId));
            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateBySystem(string paymentMethodId)
        {
            Emit(new BillingItemStatusUpdatedEvent("Validated", this.Id.Value));
            Emit(new BillingItemSystemValidatedEvent(this.Id.Value, paymentMethodId));
            return ExecutionResult.Success();
        }

        public IExecutionResult PaymentFailure(string paymentMethodId)
        {
            Emit(new BillingItemStatusUpdatedEvent("Failed", this.Id.Value));
            Emit(new PaymentFailedEvent(this.Id.Value, paymentMethodId, "Your payment has failed, Please check your card details", DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public void Apply(BillingItemCreatedEvent @event) 
        {
            this.subscriptionType = @event.SubscriptionType;
        }

        public void Apply(InvoiceGeneratedEvent @event) { }

        public void Apply(SubscriptionNPaymentToBillingLinkedEvent @event)
        {
            this.subscriptionRequestId = @event.SubscriptionRequestId;
            this.paymentMethodId = @event.PaymentMethodId;
        }

        public void Apply(BillingItemSystemValidatedEvent @event) { }

        public void Apply(BillingItemStatusUpdatedEvent @event)
        {
            this.status = @event.Status;
        }
        public void Apply(PaymentFailedEvent @event) { }
        public void Apply(BillingItemSystemDeclinedEvent @event) { }
        public void Apply(PaymentExecutionInitializedEvent @event) 
        {
            this.billingItemId = @event.ItemId;
            this.merchantTransactionId = @event.MerchantTransactionId;
            this.subscriptionType = @event.Type;
        }

        internal IExecutionResult LinkSubscriptionNPaymentToBilling(string subscriptionRequestId, string paymentMethodId, string paymentMethodStatus, object subscriptionType)
        {
            throw new NotImplementedException();
        }
    }
}
