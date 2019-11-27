using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands
{
    public class SubscriptionRequestLinkToSubscriptionAccountCommand : Command<SubscriptionAccount, SubscriptionAccountId>
    {
        public string SubscriptionRequestId { get; }
        public string SubscriptionRequestStatus { get; }
        public string SubscriptionRequestType { get; }

        public SubscriptionRequestLinkToSubscriptionAccountCommand(SubscriptionAccountId aggregateId, string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType ) : base(aggregateId)
        {
            if (string.IsNullOrEmpty(subscriptionRequestId))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionRequestId));
            }

            if (string.IsNullOrEmpty(subscriptionRequestStatus))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionRequestStatus));
            }

            if (string.IsNullOrEmpty(subscriptionRequestType))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionRequestType));
            }

            SubscriptionRequestId = subscriptionRequestId;
            SubscriptionRequestStatus = subscriptionRequestStatus;
            SubscriptionRequestType = subscriptionRequestType;
        }


    }
}
