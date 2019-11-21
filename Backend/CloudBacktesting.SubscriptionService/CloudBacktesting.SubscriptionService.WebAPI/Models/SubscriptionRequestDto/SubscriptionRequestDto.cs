using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.SubscriptionRequestDto
{
    public class CreateSubscriptionRequestDto
    {
        public string Status { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
