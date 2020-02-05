using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Models.PaymentAccount
{
    public class PaymentAccountReadModelDto
    {
        public string Id { get; set; }
        public string Client { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
