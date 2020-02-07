using CloudBacktesting.PaymentService.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.PaymentService.WebAPI.Models.BillingItem
{
    public class ExecutePaymentDto
    {
        public Card CardDetails { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
