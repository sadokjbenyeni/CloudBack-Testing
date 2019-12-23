using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBacktesting.Infra.Security.Authorization
{
    public class CloudBacktestingAuthorizationHandler : AuthorizationHandler<PoliciesAuthorizationRequirement>
    {
        private readonly ILogger<CloudBacktestingAuthenticationHandler> logger;

        public CloudBacktestingAuthorizationHandler(ILogger<CloudBacktestingAuthenticationHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PoliciesAuthorizationRequirement requirement)
        {
            // Log as a warning so that it's very clear in sample output which authorization policies 
            // (and requirements/handlers) are in use
            logger.LogInformation("Authorization validating...");
            logger.LogDebug("Evaluating authorization requirement for Policies includes {0}", string.Join(requirement.Constraint.ToString(), requirement.GetPolicies()));

            // Check the user's age
            //var dateOfBirthClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);
            if (string.IsNullOrEmpty(requirement.Policies))
            {
                logger.LogWarning("This part of API need to be authentificated");
                logger.LogDebug("{0} tried to connect at {1}", context.User?.Identity?.Name ?? "N/A", context.Resource?.ToString());
                logger.LogDebug("User's policies: {0}{1}",Environment.NewLine, string.Join(Environment.NewLine, context.User?.Claims.Select(claim => claim.Type)));
                logger.LogDebug("Policies required: {0}{1}", Environment.NewLine, string.Join(Environment.NewLine, requirement.GetPolicies()));
                context.Fail();
            }
            if (requirement.IsValid(context.User.Claims))
            {
                logger.LogInformation("Authorization succeed");
                context.Succeed(requirement);
            }
            else
            {
                logger.LogWarning("Authorization failed");
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
