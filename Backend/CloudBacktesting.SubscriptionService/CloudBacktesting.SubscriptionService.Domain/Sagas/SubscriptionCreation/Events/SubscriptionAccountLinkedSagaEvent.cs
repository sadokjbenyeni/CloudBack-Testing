using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionCreation.Events
{
    public class SubscriptionAccountLinkedSagaEvent : AggregateEvent<SubscriptionCreationSaga, SubscriptionCreationSagaId>, ISubscriptionSagaRequestId
    {
        public string SubscriptionRequestStatus { get; }
        public string SubscriptionRequestType { get; }
        public string RequestId { get; }

        public SubscriptionAccountLinkedSagaEvent(string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType)
        {
            RequestId = subscriptionRequestId;
            SubscriptionRequestStatus = subscriptionRequestStatus;
            SubscriptionRequestType = subscriptionRequestType;
        }
    }
}
