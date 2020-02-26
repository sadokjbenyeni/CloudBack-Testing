using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Specifications;
using CloudBacktesting.PaymentService.Infra.Models;
using CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate
{
    public class PaymentMethod : AggregateRoot<PaymentMethod, PaymentMethodId>, IEmit<PaymentMethodCreatedEvent>

    {

        private string paymentAccountId;
        private string paymentMethodStatus;
        private string paymentMethodSubscriber;
        private Card paymentMethodCardDetails;



        public PaymentMethod(PaymentMethodId aggregateId) : base(aggregateId)
        {
        }

        public IExecutionResult Create(string paymentAccountId, string cardNumber, string cardType, string cardHolder, string expirationYear, string expirationMonth, string cryptogram)
        {
            var @event = new PaymentMethodCreatedEvent(this.Id.Value, paymentAccountId, "Creating", cardNumber, cardType, cardHolder, cryptogram, expirationYear, expirationMonth);
            Emit(@event);
            return ExecutionResult.Success();
        }

        public IExecutionResult ValidateBySystem(PaymentMethodId paymentMethodId, string client, string cardNumber, string cardType, string cryptogram, string expirationYear, string expirationMonth)
        {
            var passLuhenSpec = new PassesLuhenTestSpecification();
            if (passLuhenSpec.IsSatisfiedBy(cardNumber) == false)
            {
                return ExecutionResult.Failed(passLuhenSpec.WhyIsNotSatisfiedBy("Card didn't pass Luhen algorithm"));
            }
            var getCardType = new GetCardTypeFromNumber();
            if (getCardType.GetCardType(cardNumber).Value.ToString() != cardType)
            {
                return ExecutionResult.Failed("Card type provided is not correct");
            }
            var isNotNull = new IsNotNullCryptogram();
            if (isNotNull.IsSatisfiedBy(cryptogram) == false)
            {
                return ExecutionResult.Failed(isNotNull.WhyIsNotSatisfiedBy("Cryptogram can't be null"));
            }
            Emit(new PaymentAccountAffectedEvent(client));
            Emit(new PaymentMethodStatusUpdatedEvent("Validated", paymentAccountId.ToString()));
            Emit(new PaymentMethodValidatedEvent(this.Id.Value, DateTime.UtcNow));
            return ExecutionResult.Success();
        }
        public IExecutionResult RejectBySystem(PaymentMethodId paymentMethod, string client)
        {
            Emit(new PaymentMethodStatusUpdatedEvent("Declined", paymentMethod.ToString()));
            Emit(new PaymentMethodDeclinedEvent(this.Id.Value, this.paymentAccountId, client, "You have been rejected by the system, Please check your card information", DateTime.UtcNow));
            return ExecutionResult.Success();
        }

        public IExecutionResult LinkBillingItem(string billingItemId, string type)
        {
            Emit(new BillingItemLinkedEvent(billingItemId, this.paymentMethodStatus, this.paymentMethodSubscriber, this.paymentMethodCardDetails, type));
            return ExecutionResult.Success();
        }

        public void Apply(PaymentMethodCreatedEvent @event)
        {
            this.paymentAccountId = @event.PaymentAccountId;
        }
        public void Apply(PaymentMethodValidatedEvent @event) { }
        public void Apply(PaymentAccountAffectedEvent @event) { }

        public void Apply(BillingItemLinkedEvent @event)
        {
            this.paymentMethodStatus = @event.PaymentMethodStatus;
            this.paymentMethodSubscriber = @event.PaymentMethodSubscriber;
            this.paymentMethodCardDetails = @event.PaymentMethodCardDetails;
        }
        public void Apply(PaymentMethodStatusUpdatedEvent @event) { }
    }
}
