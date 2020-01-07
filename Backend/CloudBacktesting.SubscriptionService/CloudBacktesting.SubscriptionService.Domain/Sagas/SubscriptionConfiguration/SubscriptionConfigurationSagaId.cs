using EventFlow.Core;
using EventFlow.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionConfiguration
{
    public class SubscriptionConfigurationSagaId : Identity<SubscriptionConfigurationSagaId>, ISagaId
    {
        public SubscriptionConfigurationSagaId(string value) : base(value){ }
    }
}
