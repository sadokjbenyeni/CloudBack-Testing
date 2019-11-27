using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.SubscriptionRequest
{
    public class CreateSubscriptionRequestDto
    {
        public string Status { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public string SubscriptionAccountId { get; set; }
    }
    public class UpdateSubscriptionRequestDto
    {
        public string SubscriptionId { get; set; }
        public string Status { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
