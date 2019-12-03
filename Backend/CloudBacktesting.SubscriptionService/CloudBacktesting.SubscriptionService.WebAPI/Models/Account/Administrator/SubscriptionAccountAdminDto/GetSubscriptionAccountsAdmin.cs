using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Account.Administrator.SubscriptionAccountAdminDto
{

    public class SubscriptionAccountAdminListItem
    {
        public string Subscriber { get; set; }
    }


    public class GetSubscriptionAccountsAdmin : IQuery<List<SubscriptionAccountAdminListItem>>
    {

    }
}
