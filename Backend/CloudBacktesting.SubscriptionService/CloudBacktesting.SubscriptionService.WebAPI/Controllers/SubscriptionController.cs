using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudBacktesting.SubscriptionService.WebAPI.Models;
using CloudBacktesting.SubscriptionService.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudBacktesting.SubscriptionService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionAccountService _subscriptionService;

        public SubscriptionController(SubscriptionAccountService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public ActionResult<List<SubscriptionAccount>> Get() =>
            _subscriptionService.Get();

        [HttpGet("{id:length(24)}", Name = "GetSubscription")]
        public ActionResult<SubscriptionAccount> Get(string id)
        {
            var subscription = _subscriptionService.Get(id);

            if (subscription == null)
            {
                return NotFound();
            }

            return subscription;
        }

        [HttpPost]
        public ActionResult<SubscriptionAccount> Create(SubscriptionAccount subscription)
        {
            _subscriptionService.Create(subscription);

            return CreatedAtRoute("GetSubscription", new { id = subscription.Id.ToString() }, subscription);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, SubscriptionAccount subscriptionIn)
        {
            var subscription = _subscriptionService.Get(id);

            if (subscription == null)
            {
                return NotFound();
            }

            _subscriptionService.Update(id, subscriptionIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var subscription = _subscriptionService.Get(id);

            if (subscription == null)
            {
                return NotFound();
            }

            _subscriptionService.Remove(subscription.Id);

            return NoContent();
        }
    }
}
