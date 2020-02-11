using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas
{
    public interface IBillingSagaItemId
    {
        string ItemId { get; }
    }
}
