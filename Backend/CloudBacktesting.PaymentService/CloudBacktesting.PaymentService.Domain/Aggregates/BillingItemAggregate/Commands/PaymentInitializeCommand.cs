using CloudBacktesting.PaymentService.Infra.Models;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class PaymentInitializeCommand : Command<BillingItem, BillingItemId, IExecutionResult>
    {
        public string SubscriptionType { get; set; }
        public string Subscriber { get; set; }
        public Card CreditCard { get; set; }
        public PaymentInitializeCommand(BillingItemId billingItemId, string subscriptionType, string subscriber, Card creditCard) : base(billingItemId)
        {
            SubscriptionType = subscriptionType;
            Subscriber = subscriber;
            CreditCard = creditCard;
        }
    }
}
