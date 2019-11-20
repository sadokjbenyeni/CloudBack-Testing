using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga.Events;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionSaga
{
    public class SubscriptionCreationSagaState 
        //: SagaState<SubscriptionCreationSaga, SubscriptionCreationSagaId, IMessageApplier<SubscriptionCreationSaga, SubscriptionCreationSagaId>>,
        //IApply<SubscriptionCreationStartedEvent>,
        //IApply<SubscriptionCreationProgressEvent>,
        //IApply<SubscriptionCreationEndedEvent>
    {
        public int Progress { get; private set; }

        public void Apply(SubscriptionCreationStartedEvent aggregateEvent)
        {
            Progress = 0;
        }

        public void Apply(SubscriptionCreationProgressEvent aggregateEvent)
        {
            Progress = aggregateEvent.Progress;
        }

        public void Apply(SubscriptionCreationEndedEvent aggregateEvent)
        {
            Progress = 100;
        }
    }
}
