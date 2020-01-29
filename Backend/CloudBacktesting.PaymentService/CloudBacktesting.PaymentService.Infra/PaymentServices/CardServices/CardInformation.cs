using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Infra.PaymentServices.CardServices
{
    public class CardInformation
    {
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string HolderName { get; set; }
        public string Number { get; set; }
        public string SecurityCode { get; set; }

    }
}
