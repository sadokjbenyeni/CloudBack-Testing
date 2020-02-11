using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Infra.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static Claim GetUserIdentifier(this ClaimsPrincipal principal)
        {
            return principal.FindFirst("subscriptionaccountid");
        }

        public static Claim GetUserPaymentIdentifier(this ClaimsPrincipal principal)
        {
            return principal.FindFirst("paymentaccountid");
        }
    }
}
