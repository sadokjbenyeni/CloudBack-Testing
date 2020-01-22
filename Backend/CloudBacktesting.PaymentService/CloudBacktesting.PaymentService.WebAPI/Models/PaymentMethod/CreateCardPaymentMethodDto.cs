using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Models.PaymentMethod
{
    public class CreateCardPaymentMethodDto
    {
        public string Holder { get; set; }
        public string Numbers { get; set; }
        public string Network { get; set; }
        public string Cryptogram { get; set; }
        public string ExpirationDate { get; set; }
    }
}
