using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation.Events
{
    public class SubscriptionCreationSagaCompletedEvent : AggregateEvent<SubscriptionCreationSaga, SubscriptionCreationSagaId>
    {

    }
}
