using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Models.BillingItem
{
    public class BillingItemReadModelDto
    {
        public string Id { get; set; }
        public string PaymentMethodId { get; set; }
        public string SubscriptionRequestId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
