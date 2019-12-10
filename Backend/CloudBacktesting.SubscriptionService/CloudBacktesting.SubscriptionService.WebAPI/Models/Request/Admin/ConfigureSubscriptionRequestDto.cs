using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Admin
{
    public class ConfigureSubscriptionRequestDto
    {
        public string Id { get; set; }
        public string Subscriber { get; set; }
    }
}
