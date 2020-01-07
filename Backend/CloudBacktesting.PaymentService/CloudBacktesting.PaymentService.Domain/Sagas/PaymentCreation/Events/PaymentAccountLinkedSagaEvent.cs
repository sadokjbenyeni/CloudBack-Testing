﻿using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Sagas.PaymentCreation.Events
{
    public class PaymentAccountLinkedSagaEvent : AggregateEvent<PaymentCreationSaga, PaymentCreationSagaId>, IPaymentSagaMethodId
    {
        public string PaymentMethodCardNumber { get; }
        public string PaymentMethodCardType { get; }
        public string PaymentMethodCryptogram { get; }
        public string MethodId { get; }
        public DateTime ExpirationDate { get; }

        public PaymentAccountLinkedSagaEvent( string methodId, string paymentMethodCardNumber, string paymentMethodCardType, string paymentMethodCryptogram, DateTime expirationDate)
        {
            MethodId = methodId;
            PaymentMethodCardNumber = paymentMethodCardNumber;
            PaymentMethodCardType = paymentMethodCardType;
            PaymentMethodCryptogram = paymentMethodCryptogram;
            PaymentMethodCryptogram = paymentMethodCryptogram;
        }
    }
}