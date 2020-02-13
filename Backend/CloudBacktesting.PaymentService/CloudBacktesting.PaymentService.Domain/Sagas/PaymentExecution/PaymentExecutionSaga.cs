using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands;
using CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Events;
using CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution.Events;
using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentExecution
{
    //public class PaymentExecutionSaga : AggregateSaga<PaymentExecutionSaga, PaymentExecutionSagaId, PaymentExecutionSagaLocator>,
    //                                    ISagaIsStartedBy<BillingItem, BillingItemId, PaymentExecutedEvent>,
    //                                    ISagaHandles<BillingItem, BillingItemId, InvoiceGeneratedEvent>
    //{
    //    public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, PaymentExecutedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    //    {
    //        var command = new PaymentExecutionCommand(domainEvent.AggregateIdentity, domainEvent.AggregateEvent.MerchantTransactionId, domainEvent.AggregateEvent.Subscriber, domainEvent.AggregateEvent.CardDetails, domainEvent.AggregateEvent.Amount, domainEvent.AggregateEvent.Currency);
    //        this.Publish(command);
    //        if (domainEvent.AggregateEvent.IsPaymentSuccessful)
    //        {
    //            var failCommand = new PaymentExecutionFailureCommand(domainEvent.AggregateIdentity.Value, domainEvent.AggregateEvent.PaymentMethodId);
    //            this.Publish(failCommand);
    //            this.Emit(new PaymentExecutedSagaEvent())
    //        }
    //        return Task.CompletedTask;
    //    }

    //    public Task HandleAsync(IDomainEvent<BillingItem, BillingItemId, InvoiceGeneratedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    //    {
    //        {
    //            var command = new InvoiceGenerationCommand(domainEvent.AggregateIdentity.Value,
    //                                       domainEvent.AggregateEvent.InvoiceId,
    //                                       domainEvent.AggregateEvent.Method,
    //                                       domainEvent.AggregateEvent.Client,
    //                                       domainEvent.AggregateEvent.CardHolder,
    //                                       domainEvent.AggregateEvent.Address,
    //                                       domainEvent.AggregateEvent.Amount,
    //                                       domainEvent.AggregateEvent.InvoiceDate);
    //            this.Publish(command);
    //            this.Emit(new PaymentExecutionSagaCompletedEvent());
    //        }

    //        return Task.CompletedTask;
    //    }
    //}
}
