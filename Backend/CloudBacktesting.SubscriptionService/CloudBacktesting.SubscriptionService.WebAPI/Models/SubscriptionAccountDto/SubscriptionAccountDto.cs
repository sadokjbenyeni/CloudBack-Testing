using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models
{
    public class CreateSubscriptionAccountDto
    {
        public string Subscriber { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
