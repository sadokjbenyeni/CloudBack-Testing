using System;
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
        public string Client { get; set; }
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public string Cryptogram { get; set; }
        public string ExpirationYear { get; set; }
        public string ExpirationMonth { get; set; }
    }
}
