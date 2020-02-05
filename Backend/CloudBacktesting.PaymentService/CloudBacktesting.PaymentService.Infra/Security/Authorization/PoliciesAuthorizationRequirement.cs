using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CloudBacktesting.Infra.Security.Authorization
{
    public class PoliciesAuthorizationRequirement : IAuthorizationRequirement
    {

        public string Policies { get; set; }
        public LogicalContrainst Constraint { get; set; }

        public PoliciesAuthorizationRequirement(string policies, LogicalContrainst constraint)
        {
            Policies = policies;
            Constraint = constraint;
        }

        public IEnumerable<string> GetPolicies()
        {
            return Policies?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                           .Select(str => str.Trim());
        }

        public bool IsValid(IEnumerable<Claim> claims)
        {
            var hash = new HashSet<string>(claims.Select(claim => claim.Type), StringComparer.InvariantCultureIgnoreCase);
            if (Constraint == LogicalContrainst.Or)
            {
                return GetPolicies().Any(hash.Contains);
            }
            if (Constraint == LogicalContrainst.And)
            {
                return GetPolicies().All(hash.Contains);
            }
            return false;
        }
    }

    public enum LogicalContrainst
    {
        Or,
        And
    }
}
