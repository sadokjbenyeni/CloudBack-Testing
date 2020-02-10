using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodSystemRejectCommand : Command<PaymentMethod, PaymentMethodId>, IPaymentSagaMethodId
    {
        public string MethodId { get; set; }
        public string Client { get; set; }

        public PaymentMethodSystemRejectCommand(string methodId, string client) : base(new PaymentMethodId(methodId))
        {
            MethodId = methodId;
            Client = client;
        }
    }
}
