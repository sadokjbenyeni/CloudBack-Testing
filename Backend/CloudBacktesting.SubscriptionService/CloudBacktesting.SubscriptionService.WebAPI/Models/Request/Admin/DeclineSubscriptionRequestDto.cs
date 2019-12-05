using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Admin
{
    public class DeclineSubscriptionRequestDto
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }
}
