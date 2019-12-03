using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Client.SubscriptionRequest
{
    public class SubscriptionRequestReadModelDto
    {
        public string Id { get; set; }
        public string SubscriptionAccountId { get; set; }
        public string Status { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSystemValidated { get; set; }
    }
}
