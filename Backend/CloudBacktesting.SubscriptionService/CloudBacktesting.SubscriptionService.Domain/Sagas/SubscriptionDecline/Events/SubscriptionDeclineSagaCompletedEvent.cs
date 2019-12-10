using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionDecline.Event
{
    public class SubscriptionDeclineSagaCompletedEvent : AggregateEvent<SubscriptionDeclineSaga, SubscriptionDeclineSagaId>
    {
    }
}
