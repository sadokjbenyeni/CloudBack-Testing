using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodSystemValidateCommand : Command<PaymentMethod, PaymentMethodId>, IPaymentSagaMethodId
    {
        public string MethodId { get; set; }
        public string Client { get; set; }

        public PaymentMethodSystemValidateCommand(string aggregateId, string client) : base(new PaymentMethodId(aggregateId))
        {
            MethodId = aggregateId;
            Client = client;
        }
    }
}
