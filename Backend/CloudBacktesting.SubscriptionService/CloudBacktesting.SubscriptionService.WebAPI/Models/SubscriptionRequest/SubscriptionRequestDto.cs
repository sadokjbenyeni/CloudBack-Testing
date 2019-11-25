using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.SubscriptionRequest
{
    public class CreateSubscriptionRequestDto
    {
        public string Status { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
