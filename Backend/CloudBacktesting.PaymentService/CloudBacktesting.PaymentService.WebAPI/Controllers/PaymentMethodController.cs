using System;
using System.Collections.Generic;
using CloudBacktesting.PaymentService.WebAPI.Models;
using CloudBacktesting.PaymentService.WebAPI.Models.PaymentMethod;
using Microsoft.AspNetCore.Mvc;

namespace CloudBacktesting.PaymentService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = new List<PaymentMethodReadModelDto>()
            { new PaymentMethodReadModelDto
            {
                MethodId = "PaymentMethod-987654321",
                PaymentAccountId = "PaymentAccount-987654321",
                CardNumber = " ***-6279",
                CardType = "Visa",
                ExpirationDate = DateTime.UtcNow
            },
            new PaymentMethodReadModelDto
            {
                MethodId = "PaymentMethod-123456789",
                PaymentAccountId = "PaymentAccount-123456789",
                CardNumber = " ***-9842",
                CardType = "Visa",
                ExpirationDate = DateTime.UtcNow
            }};
           
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post()
        {
            return Ok(new IdentifierDto { Id = "PaymentMethod-987654321" });
        }
    }
}