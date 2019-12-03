using CloudBacktesting.SubscriptionService.Domain.Aggregates.SubscriptionAccountAggregate;
using System;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Administrator.SubscriptionRequestAdmin
{
    public class UpdateSubscriptionRequestAdminDto
    {
        public string Id { get; set; }
        public string SubscriptionAccountId { get; set; }
        public bool IsAdminValidated { get; set; }
    }
}
