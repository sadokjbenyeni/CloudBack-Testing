using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate.Commands
{
    public class SubscriptionRequestAdminValidateSuccessCommand : Command<SubscriptionRequest, SubscriptionRequestId>
    {
        public string SubscriptionAccountId { get; set; }
        public bool IsAdminValidated { get; set; }

        public SubscriptionRequestAdminValidateSuccessCommand(SubscriptionRequestId aggregateId, string subscriptionAccountId, bool isAdminValidated) : base(aggregateId)
        {
            SubscriptionAccountId = subscriptionAccountId;
            IsAdminValidated = isAdminValidated;
        }
    }
}
