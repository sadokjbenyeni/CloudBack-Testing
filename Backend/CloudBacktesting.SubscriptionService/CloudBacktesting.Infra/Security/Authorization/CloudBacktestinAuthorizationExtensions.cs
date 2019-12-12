using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudBacktesting.Infra.Security.Authorization
{
    public static class CloudBacktestinAuthorizationExtensions
    {
        const string POLICY_PREFIX = "CloudBacktestingPolicies";

        public static string ExtractFromPolicy(this CloudBacktestingAuthorizeAttribute source)
        {
            return ExtractPolicies(POLICY_PREFIX, source.Policy);
        }

        public static string BuildToPolicy(this CloudBacktestingAuthorizeAttribute source, string value)
        {
            return $"{POLICY_PREFIX}{value}";
        }

        public static string ExtractFromPolicy(this IAuthorizationPolicyProvider provider, string policyName)
        {
            return ExtractPolicies(POLICY_PREFIX, policyName);
        }

        public static bool IsCloudBacktestingPolicy(this IAuthorizationPolicyProvider provider, string policyName)
        {
            if(string.IsNullOrEmpty(policyName))
            {
                return false;
            }
            return policyName.StartsWith(POLICY_PREFIX, StringComparison.InvariantCultureIgnoreCase);
        }

        private static string ExtractPolicies(string prefix, string policy)
        {
            if (string.IsNullOrEmpty(policy))
            {
                return null;
            }
            var policies = policy?.Substring(prefix.Length);
            if (!string.IsNullOrEmpty(policies))
            {
                return policies;
            }
            return null;
        }
    }
}
