using CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionConfiguration;
using EventFlow.Aggregates;
using System;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionConfiguration.Events
{
    public class SubscriptionRequestConfiguredEmailSentEvent : AggregateEvent<SubscriptionConfigurationSaga, SubscriptionConfigurationSagaId>, ISubscriptionSagaRequestId
    {
        public string RequestId { get; }
        public string Message { get; }
        public string SubscriptionAccountId { get; }
        public DateTime ConfigurationDate { get; }

        public SubscriptionRequestConfiguredEmailSentEvent(string subscriptionRequestId, string message, string subscriptionAccountId, DateTime configurationDate)
        {
            RequestId = subscriptionRequestId;
            Message = message;
            SubscriptionAccountId = subscriptionAccountId;
            ConfigurationDate = configurationDate;
        }
    }
}
