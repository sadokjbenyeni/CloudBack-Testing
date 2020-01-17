using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Payment
{
    public class PaymentMethod
    {
        public string Id { get; set; }
        public string PaymentAccountId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
