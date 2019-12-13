using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccount
{
    public class SubscriptionAccountReadModelDto
    {
        public string Id { get; set; }
        public string Subscriber { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
