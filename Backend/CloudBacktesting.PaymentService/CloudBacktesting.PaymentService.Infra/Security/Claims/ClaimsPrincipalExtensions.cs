using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CloudBacktesting.PaymentService.Infra.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static Claim GetUserIdentifier(this ClaimsPrincipal principal)
        {
            return principal.FindFirst("paymentaccountid");
            //return principal.Claims.FirstOrDefault(claim => string.Equals(claim.Type, ClaimTypes.NameIdentifier));
        }
    }
}
