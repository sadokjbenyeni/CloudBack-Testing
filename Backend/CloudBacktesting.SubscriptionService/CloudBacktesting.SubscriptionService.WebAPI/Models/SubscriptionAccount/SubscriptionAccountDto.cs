using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models
{
    public class CreateSubscriptionAccountDto
    {
        public string Subscriber { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }

    
}
