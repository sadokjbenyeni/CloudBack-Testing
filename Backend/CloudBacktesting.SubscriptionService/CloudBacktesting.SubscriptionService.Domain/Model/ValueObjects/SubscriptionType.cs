using Akkatecture.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects
{
    public class SubscriptionType : SingleValueObject<string>
    {
        public SubscriptionType( string value) : base(value) 
        {
        }
    }
}
