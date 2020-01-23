using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate.Commands
{
    public class PaymentMethodCreationCommand : Command<PaymentMethod, PaymentMethodId, IExecutionResult>
    {
        public string PaymentAccountId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public string ExpirationDate { get; set; }

        public PaymentMethodCreationCommand(string paymentAccountId, string cardNumber, string cardType, string cardHolder, string expirationDate) : base(PaymentMethodId.New)
        {
            if (string.IsNullOrEmpty(paymentAccountId))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(paymentAccountId));
            }

            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(cardNumber));
            }

            if (string.IsNullOrEmpty(cardType))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(cardType));
            }

            if (string.IsNullOrEmpty(cardHolder))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(cardHolder));
            }

            if (string.IsNullOrEmpty(expirationDate))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(expirationDate));
            }

            PaymentAccountId = paymentAccountId;
            CardNumber = cardNumber;
            CardType = cardType;
            CardHolder = cardHolder;
            ExpirationDate = expirationDate;
        }
    }
}
