using Akkatecture.Aggregates;
using Akkatecture.Sagas;
using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga
{
    public class SubscriptionAccountCreationSagaState : SagaState<SubscriptionAccountCreationSaga, SubscriptionAccountCreationSagaId, IMessageApplier<SubscriptionAccountCreationSaga, SubscriptionAccountCreationSagaId>>,
        IApply<SubscriptionAccountCreationStartedEvent>,
        IApply<SubscriptionAccountCreationProgressEvent>,
        IApply<SubscriptionAccountCreationEndedEvent>
    {
        public int Progress { get; private set; }

        public void Apply(SubscriptionAccountCreationStartedEvent aggregateEvent)
        {
            Progress = 0;
        }

        public void Apply(SubscriptionAccountCreationProgressEvent aggregateEvent)
        {
            Progress = aggregateEvent.Progress;
        }

        public void Apply(SubscriptionAccountCreationEndedEvent aggregateEvent)
        {
            Progress = 100;
        }
    }
}
