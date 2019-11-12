using Akkatecture.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects
{
    public class SubscriptionDate : SingleValueObject<DateTime>
    {
        public SubscriptionDate(DateTime value) : base(value)
        {
        }
    }
}
