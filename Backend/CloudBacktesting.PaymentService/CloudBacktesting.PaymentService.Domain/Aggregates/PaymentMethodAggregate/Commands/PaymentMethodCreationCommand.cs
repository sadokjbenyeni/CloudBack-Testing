﻿using EventFlow.Aggregates.ExecutionResults;
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
        public DateTime ExpirationDate { get; set; }

        public PaymentMethodCreationCommand(string paymentAccountId, string cardNumber, string cardType, string cardHolder, DateTime expirationDate) : base(PaymentMethodId.New)
        {
            PaymentAccountId = paymentAccountId;
            CardNumber = cardNumber;
            CardType = cardType;
            CardHolder = cardHolder;
            ExpirationDate = expirationDate;
        }
    }
}