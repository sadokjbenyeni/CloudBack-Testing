using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation.Events
{
    public class SubscriptionAccountLinkedSagaEvent : AggregateEvent<SubscriptionCreationSaga, SubscriptionCreationSagaId>
    {
        public string SubscriptionRequestId { get; }
        public string SubscriptionRequestStatus { get; }
        public string SubscriptionRequestType { get; }

        public SubscriptionAccountLinkedSagaEvent(string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType)
        {
            SubscriptionRequestId = subscriptionRequestId;
            SubscriptionRequestStatus = subscriptionRequestStatus;
            SubscriptionRequestType = subscriptionRequestType;
        }
    }
}
