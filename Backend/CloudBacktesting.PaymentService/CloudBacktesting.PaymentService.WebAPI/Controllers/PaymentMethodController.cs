using System;
using System.Collections.Generic;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentAccountAggregate;
using CloudBacktesting.PaymentService.Domain.Aggregates.PaymentMethodAggregate;
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
                MethodId = PaymentMethodId.New.ToString(),
                PaymentAccountId = PaymentAccountId.New.ToString(),
                CardHolder = "TIM COOK",
                CardNumber = " ***-6279",
                CardType = "Visa",
                ExpirationDate = DateTime.UtcNow
            },
            new PaymentMethodReadModelDto
            {
                MethodId = PaymentMethodId.New.ToString(),
                PaymentAccountId = PaymentAccountId.New.ToString(),
                CardHolder = "ELON MUSK",
                CardNumber = " ***-9842",
                CardType = "Visa",
                ExpirationDate = DateTime.UtcNow
            }};

            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody] PaymentMethodDto value)
        {
            return Ok(new PaymentMethodDto
            {
                CardNumber = value.CardNumber,
                CardType = value.CardType,
                CardHolder = value.CardHolder,
                PaymentAccountId = value.PaymentAccountId,
                ExpirationDate = value.ExpirationDate
            });
        }
    }
}