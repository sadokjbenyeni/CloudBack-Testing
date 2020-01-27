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
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string Cryptogram { get; set; }
        public int ExpirationYear { get; set; }
        public int ExpirationMonth { get; set; }
        public PaymentMethodSystemValidateCommand(string aggregateId, string client, string cardNumber, string cardType, int expirationYear, int expirationMonth, string cryptogram) : base(new PaymentMethodId(aggregateId))
        {
            MethodId = aggregateId;
            Client = client;
            CardNumber = cardNumber;
            CardType = cardType;
            Cryptogram = cryptogram;
            ExpirationYear = expirationYear;
            ExpirationMonth = expirationMonth;
        }
    }
}
