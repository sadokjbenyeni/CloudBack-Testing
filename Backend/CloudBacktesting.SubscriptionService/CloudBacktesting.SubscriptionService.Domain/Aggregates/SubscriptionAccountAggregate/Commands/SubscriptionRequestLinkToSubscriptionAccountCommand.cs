using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionRequestAggregate;
using CloudBacktesting.SubscriptionService.Domain.Sagas;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate.Commands
{
    public class SubscriptionRequestLinkToSubscriptionAccountCommand : Command<SubscriptionAccount, SubscriptionAccountId>, ISubscriptionSagaRequestId
    {
        public string RequestId { get; }
        public string SubscriptionRequestStatus { get; }
        public string SubscriptionRequestType { get; }
        public string PaymentMethodId { get; set; }
        public string PaymentAccountId { get; set; }

        public SubscriptionRequestLinkToSubscriptionAccountCommand(SubscriptionAccountId subscriptionAccountId, string subscriptionRequestId, string subscriptionRequestStatus, string subscriptionRequestType, string paymentMethodId, string paymentAccountId) : base(subscriptionAccountId)
        {
            if (string.IsNullOrEmpty(subscriptionRequestId))
            {
                throw new ArgumentException("Cannot ve empty", nameof(subscriptionRequestId));
            }

            if (string.IsNullOrEmpty(subscriptionRequestStatus))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionRequestStatus));
            }

            if (string.IsNullOrEmpty(subscriptionRequestType))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(subscriptionRequestType));
            }
            if (string.IsNullOrEmpty(paymentAccountId))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(paymentAccountId));
            }
            if (string.IsNullOrEmpty(paymentMethodId))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(paymentMethodId));
            }
            RequestId = subscriptionRequestId;
            SubscriptionRequestStatus = subscriptionRequestStatus;
            SubscriptionRequestType = subscriptionRequestType;
            PaymentAccountId = paymentAccountId;
            PaymentMethodId = paymentAccountId;
        }
    } 
}
