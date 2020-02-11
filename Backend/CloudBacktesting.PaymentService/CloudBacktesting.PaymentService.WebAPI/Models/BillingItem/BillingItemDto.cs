using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Models.BillingItem
{
    public class BillingItemDto
    {
        public string PaymentMethodId { get; set; }
        public string SubscriptionRequestId { get; set; }
        public string PaymentMethodStatus { get; internal set; }
    }
}
