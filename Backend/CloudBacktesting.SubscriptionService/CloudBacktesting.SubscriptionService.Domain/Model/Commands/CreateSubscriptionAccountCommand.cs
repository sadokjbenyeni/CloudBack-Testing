using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Commands
{
    public class CreateSubscriptionAccountCommand
    {
        public string UserIdentifier { get; set; }
    }
}
