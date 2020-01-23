using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Client.SubscriptionRequest
{
    public class SubscriptionRequestReadModelDto
    {
        public string Id { get; set; }
        public string SubscriptionAccountId { get; set; }
        public long? Version { get; set; }
        public string Subscriber { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsSystemValidated { get; set; } = null;
        public bool? IsManualValidated { get; set; } = null;
        public string DeclineMessage { get; set; }
        public DateTime? ValidatedOrDeclinedDate { get; set; } = null;
        public DateTime? RejectedDate { get; set; } = null;
        public bool IsManualConfigured { get; set; } = false;
        public string ActivationMessage { get; set; }
        public DateTime? ActivatedDate { get; set; } = null;
        public string PaymentMethodId { get; set; }
    }
}
