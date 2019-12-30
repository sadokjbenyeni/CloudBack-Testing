using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.BillingItemAggregate.Commands
{
    public class BillingItemCreationCommand : Command<BillingItem, BillingItemId, IExecutionResult>
    {
        public string PayementMethodId { get; set; }

        public BillingItemCreationCommand(string payemntMethodId) : base(BillingItemId.New)
        {
            PayementMethodId = payemntMethodId ?? throw new ArgumentNullException(nameof(payemntMethodId));
        }
    }
}
