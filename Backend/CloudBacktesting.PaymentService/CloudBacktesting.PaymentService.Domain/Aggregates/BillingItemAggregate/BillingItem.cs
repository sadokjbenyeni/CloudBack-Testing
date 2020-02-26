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
    public class BillingItem : AggregateRoot<BillingItem, BillingItemId>, IEmit<BillingItemCreatedEvent>, IEmit<PaymentExecutionInitializedEvent>
    {
        private string subscriptionType;
        private string merchantTransactionId;
        private string paymentMethodId;
        private string subscriptionRequestId;
        private string status;
        private string subscriber;
        private Card cardDetails;
        private readonly ISmart2PayCardService _smart2payCardService;

        public BillingItem(BillingItemId aggregateId, ISmart2PayCardService smart2PayCardService) : base(aggregateId)
        {
            _smart2payCardService = smart2PayCardService;
        }

        public IExecutionResult Create(string paymentMethodId, string paymentMethodStatus, string subscriptionRequestId, string subscriptionType)
        {
            Emit(new BillingItemCreatedEvent(this.Id.Value, paymentMethodId, subscriptionRequestId, "Creating", paymentMethodStatus, DateTime.UtcNow, subscriptionType));
            return ExecutionResult.Success();
        }

        public IExecutionResult LinkSubscriptionNPaymentToBilling(string billingItemId, string paymentMethodStatus)
        {
            Emit(new SubscriptionNPaymentToBillingLinkedEvent(billingItemId, this.subscriptionRequestId, this.paymentMethodId, paymentMethodStatus, this.subscriptionType));
            return ExecutionResult.Success();
        }

        public IExecutionResult GenerateInvoice(string billingItemId, string invoiceId, string client, string cardHolder)
        {
            Emit(new InvoiceGeneratedEvent(billingItemId, invoiceId, client, cardHolder, DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public IExecutionResult InitializePayment(string subscriber, Card cardDetails)
        {
            Emit(new PaymentExecutionInitializedEvent(this.Id.Value, this.paymentMethodId, new MerchantTransaction().Id, this.subscriptionType, subscriber, cardDetails));
            return ExecutionResult.Success();

        }
        public IExecutionResult ExecutePayment(string billingItemId, string paymentMethodId, string merchantTransactionId, string subscriber, string type, Card cardDetails)
        {
            var response = _smart2payCardService.CreateAsync(merchantTransactionId, subscriber, cardDetails, type, "EUR", CancellationToken.None);
            Emit(new PaymentExecutedEvent(merchantTransactionId, billingItemId, paymentMethodId, this.subscriber, this.cardDetails, type, "EUR", response.Result));
            return ExecutionResult.Success();
        }
        public IExecutionResult DeclineBySystem(string billingItemId, string paymentMethodId)
        {
            Emit(new BillingItemStatusUpdatedEvent("Declined", billingItemId));
            Emit(new BillingItemSystemDeclinedEvent(this.Id.Value, paymentMethodId));
            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateBySystem(string billingItemId, string paymentMethodId)
        {
            Emit(new BillingItemStatusUpdatedEvent("Validated", billingItemId));
            Emit(new BillingItemSystemValidatedEvent(this.Id.Value, paymentMethodId));
            return ExecutionResult.Success();
        }

        public IExecutionResult PaymentFailure(string billingItemId, string paymentMethodId)
        {
            Emit(new BillingItemStatusUpdatedEvent("Failed", billingItemId));
            Emit(new PaymentFailedEvent(this.Id.Value, paymentMethodId, "Your payment has failed, Please check your card details", DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public void Apply(BillingItemCreatedEvent @event)
        {
            this.subscriptionType = @event.SubscriptionType;
            this.subscriptionRequestId = @event.SubscriptionRequestId;
            this.paymentMethodId = @event.PaymentMethodId;
        }

        public void Apply(InvoiceGeneratedEvent @event) { }

        public void Apply(SubscriptionNPaymentToBillingLinkedEvent @event)
        {

        }

        public void Apply(BillingItemSystemValidatedEvent @event) { }
        public void Apply(PaymentExecutedEvent @event) { }

        public void Apply(BillingItemStatusUpdatedEvent @event)
        {
            this.status = @event.Status;
        }
        public void Apply(PaymentFailedEvent @event) { }
        public void Apply(BillingItemSystemDeclinedEvent @event) { }
        public void Apply(PaymentExecutionInitializedEvent @event)
        {
            this.merchantTransactionId = @event.MerchantTransactionId;
            this.subscriptionType = @event.SubscriptionType;
            this.subscriber = @event.Subscriber;
            this.cardDetails = @event.CreditCard;
        }
    }
}
