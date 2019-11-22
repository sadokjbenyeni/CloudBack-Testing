using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.SubscriptionAccountDto
{

    public class SubscriptionAccountListItem
    {
        public string Subscriber { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }


    public class GetSubscriptionAccounts : IQuery<List<SubscriptionAccountListItem>>
    {

    }
}
