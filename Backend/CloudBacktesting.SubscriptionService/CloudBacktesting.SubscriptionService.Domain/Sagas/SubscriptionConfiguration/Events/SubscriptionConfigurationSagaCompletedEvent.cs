using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionConfiguration;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionConfiguration.Events
{
    public class SubscriptionConfigurationSagaCompletedEvent : AggregateEvent<SubscriptionConfigurationSaga, SubscriptionConfigurationSagaId>
    {
    }
}
