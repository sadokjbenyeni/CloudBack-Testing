using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace CloudBacktesting.PaymentService.Infra.Models
{
    public class MerchantTransaction  
    {
        public string Id { get; set; }

        public MerchantTransaction()
        {
            Id = $"MerchantTransaction-{Guid.NewGuid().ToString()}{DateTime.UtcNow.Year}{DateTime.UtcNow.Month}{DateTime.UtcNow.Day}";
        }
    }
}
