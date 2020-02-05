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
        public string PaymentMethodExpirationYear { get; set; }
        public string PaymentMethodExpirationMonth { get; set; }
        public PaymentMethodLinkToPaymentAccountCommand(PaymentAccountId paymentAccountId, string methodId, string paymentMethodCardNumber, string paymentMethodCardType, string paymentMethodCryptogram, string paymentMethodExpirationYear, string paymentMethodExpirationMonth) : base(paymentAccountId)
        {
            if (string.IsNullOrEmpty(methodId))
            {
                throw new ArgumentException("Cannot be empty", nameof(methodId));
            }

            if (string.IsNullOrEmpty(paymentMethodCardNumber))
            {
                throw new ArgumentException("Cannot be empty", nameof(paymentMethodCardNumber));
            }

            if (string.IsNullOrEmpty(paymentMethodCardType))
            {
                throw new ArgumentException("Cannot be empty", nameof(paymentMethodCardType));
            }

            if (string.IsNullOrEmpty(paymentMethodCryptogram))
            {
                throw new ArgumentException("Cannot be empty", nameof(paymentMethodCryptogram));
            }

            MethodId = methodId;
            PaymentMethodCardNumber = paymentMethodCardNumber;
            PaymentMethodCardType = paymentMethodCardType;
            PaymentMethodCryptogram = paymentMethodCryptogram;
            PaymentMethodExpirationYear = paymentMethodExpirationYear;
            PaymentMethodExpirationMonth = paymentMethodExpirationMonth;
        }
    }
}
