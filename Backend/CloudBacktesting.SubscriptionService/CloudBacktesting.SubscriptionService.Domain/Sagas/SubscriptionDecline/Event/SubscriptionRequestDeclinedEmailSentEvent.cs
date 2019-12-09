using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionDecline.Event
{
    public class SubscriptionRequestDeclinedEmailSentEvent : AggregateEvent<SubscriptionDeclineSaga, SubscriptionDeclineSagaId>, ISubscriptionSagaRequestId
    {
        public string RequestId { get; }
        public string Message { get; }
        public string SubscriptionAccountId { get; }
        public string ManualDeclinedDate { get; set; }

        public SubscriptionRequestDeclinedEmailSentEvent(string subscriptionRequestId, string message, string subscriptionAccountId, string manualDeclinedDate)
        {
            RequestId = subscriptionRequestId;
            Message = message;
            SubscriptionAccountId = subscriptionAccountId;
            ManualDeclinedDate = manualDeclinedDate;
        }
    }
}
