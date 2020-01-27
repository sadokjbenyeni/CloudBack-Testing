﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Models.PaymentMethod
{
    public class PaymentMethodReadModelDto
    {
        public string PaymentMethodId { get; set; }
        public string PaymentAccountId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public string Cryptogram { get; set; }
        public int ExpirationYear { get; set; }
        public int ExpirationMonth { get; set; }
    }
}
