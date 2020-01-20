using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain
{
    public class PaymentAction
    {
        public string PaymentMethodId { get; set; }
        public string PaymentAccountId { get; set; }
    }
}
