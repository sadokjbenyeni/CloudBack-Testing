using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.SubscriptionRequestDto
{
    public class CreateSubscriptionRequestDto
    {
        public string Status { get; }
        public string Subscriber { get; }
        public string Type { get; }
        public DateTime SubscriptionDate { get; }
    }
}
