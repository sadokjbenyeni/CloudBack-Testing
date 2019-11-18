using Akkatecture.Sagas;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas.SubscriptionAccountSaga
{
    public class SubscriptionAccountCreationSagaId : SagaId<SubscriptionAccountCreationSagaId>
    {
        public SubscriptionAccountCreationSagaId(string value) : base(value)
        {
        }
    }
}
