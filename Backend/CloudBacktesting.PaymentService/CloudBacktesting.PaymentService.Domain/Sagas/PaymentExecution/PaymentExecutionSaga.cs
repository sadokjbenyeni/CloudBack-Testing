using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution.Events;
using CloudBacktesting.PaymentService.Infra.Models;
using CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution
{
    public class PaymentExecutionSaga : AggregateSaga<PaymentExecutionSaga, PaymentExecutionSagaId, PaymentExecutionSagaLocator>,
                                        ISagaIsStartedBy<BillingItem, BillingItemId, PaymentExecutionInitializedEvent>,
                                        ISagaHandles<PaymentMethod, PaymentMethodId, BillingItemLinkedEvent>,
                                        ISagaHandles<PaymentMethod, PaymentMethodId, PaymentExecutedEvent>,
                                        ISagaHandles<BillingItem, BillingItemId, InvoiceGeneratedEvent>
    {
        private const string URIPAYMENT = "https://securetest.smart2pay.com/payments";
  

        public PaymentExecutionSaga(PaymentExecutionSagaId id) : base(id)
        {

        }


        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, PaymentExecutionInitializedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentMethodLinkToBillingItemCommand(new PaymentMethodId(domainEvent.AggregateEvent.PaymentMethodId), domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.MerchantTransactionId, domainEvent.AggregateEvent.Type);
            this.Publish(command);
            this.Emit(new PaymentInitializedSagaEvent(domainEvent.AggregateIdentity.Value,
                                                      domainEvent.AggregateEvent.MerchantTransactionId));
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, BillingItemLinkedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            var command = new PaymentExecutionCommand(domainEvent.AggregateIdentity, domainEvent.AggregateEvent.MerchantTransactionId,domainEvent.AggregateEvent.ItemId, domainEvent.AggregateEvent.SubscriptionRequestId, domainEvent.AggregateEvent.CardDetails, domainEvent.AggregateEvent.Type, domainEvent.AggregateEvent.Subscriber);
            this.Publish(command);

            //if (domainEvent.AggregateEvent.IsPaymentSuccessful)
            //{
            //    var failCommand = new PaymentExecutionFailureCommand(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.PaymentMethodId);
            //    this.Publish(failCommand);
            //}
            return Task.CompletedTask;

        }

        public Task HandleAsync(IDomainEvent<PaymentMethod, PaymentMethodId, PaymentExecutedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            //var command = new InvoiceGenerationCommand(domainEvent.AggregateIdentity.Value,
            //   domainEvent.AggregateEvent.MerchantTransactionId,
            //   domainEvent.AggregateEvent.PaymentMethodSubscriber,
            //   domainEvent.AggregateEvent.PaymentMethodCardDetails.HolderName);

            //this.Publish(command);
            //Emit(new BillingItemStatusUpdatedEvent("Activated", this.Id.Value));

            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, InvoiceGeneratedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
        {
            Emit(new PaymentExecutionSagaCompletedEvent());
            return Task.CompletedTask;
        }

        public void Apply(PaymentInitializedSagaEvent @event) { }
        public void Apply(PaymentExecutionSagaCompletedEvent @event)
        {
            Complete();
        }



    }
}
