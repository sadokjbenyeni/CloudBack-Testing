using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudBacktesting.Infra.Security.Authorization
{
    public class CloudBacktestingAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {

        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public CloudBacktestingAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (this.IsCloudBacktestingPolicy(policyName))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PoliciesAuthorizationRequirement(this.ExtractFromPolicy(policyName), LogicalContrainst.Or));
                return Task.FromResult(policy.Build());
            }
            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }


        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return GetDefaultPolicyAsync();
        }

    }
}
