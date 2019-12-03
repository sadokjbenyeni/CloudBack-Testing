using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Administrator.SubscriptionRequestAdmin
{
    public class SubscriptionRequestReadModelAdminDto
    {
        public string Id { get; set; }
        public string SubscriptionAccountId { get; set; }
        public string Status { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSystemValidated { get; set; }
        public bool IsAdminValidated { get; set; }

    }
}
