using CloudBacktesting.PaymentService.Domain.Sagas;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate.Commands
{
    public class PaymentMethodLinkToPaymentAccountCommand : Command<PaymentAccount, PaymentAccountId>, IPaymentSagaMethodId
    {
        public string MethodId { get; }
        public string PaymentMethodCardNumber { get; }
        public string PaymentMethodCardType { get; }
        public string PaymentMethodCryptogram { get; }
        public DateTime PaymentMethodExpirationDate { get; }

        public PaymentMethodLinkToPaymentAccountCommand(PaymentAccountId paymentAccountId
            , string methodId
            , string paymentMethodCardNumber
            , string paymentMethodCardType
            , string paymentMethodCryptogram
            , DateTime paymentMethodeExpirationDate) : base(paymentAccountId)
        {
            MethodId = methodId;
            PaymentMethodCardNumber = paymentMethodCardNumber;
            PaymentMethodCardType = paymentMethodCardType;
            PaymentMethodCryptogram = paymentMethodCryptogram;
            PaymentMethodExpirationDate = paymentMethodeExpirationDate;
        }
    }
}
