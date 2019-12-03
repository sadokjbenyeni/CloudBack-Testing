using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Client.SubscriptionAccountDto
{

    public class SubscriptionAccountListItem
    {
        public string Subscriber { get; set; }
    }


    public class GetSubscriptionAccountsAdmin : IQuery<List<SubscriptionAccountListItem>>
    {

    }
}
