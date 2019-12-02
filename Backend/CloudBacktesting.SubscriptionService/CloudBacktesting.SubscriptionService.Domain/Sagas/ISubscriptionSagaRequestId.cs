using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Domain.Sagas
{
    public interface ISubscriptionSagaRequestId
    {
        string RequestId { get; }
    }
}
