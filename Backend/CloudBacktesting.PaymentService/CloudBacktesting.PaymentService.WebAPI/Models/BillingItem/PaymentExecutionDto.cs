using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Models.BillingItem
{
    public class PaymentExecutionDto
    {
        public string SubscriptionRequestId { get; set; }
        public string Currency { get; set; }
    }
}
