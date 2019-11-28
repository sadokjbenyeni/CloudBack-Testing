using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models
{
    public class CreateSubscriptionAccountDto
    {
        public string Subscriber { get; set; }
    }

    public class UpdateSubscriptionAccountDto
    {
        public string Id { get; set; }
        public string Subscriber { get; set; }
    }
}
