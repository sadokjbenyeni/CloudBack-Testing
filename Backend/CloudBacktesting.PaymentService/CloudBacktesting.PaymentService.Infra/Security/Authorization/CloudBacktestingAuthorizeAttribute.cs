using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.Infra.Security.Authorization
{
    public class CloudBacktestingAuthorizeAttribute : AuthorizeAttribute
    {
        
        public CloudBacktestingAuthorizeAttribute(string policies) => Policies = policies;

        // Get or set the Age property by manipulating the underlying Policy property
        public string Policies
        {
            get => this.ExtractFromPolicy();
            set => Policy = this.BuildToPolicy(value);
        }       
    }
}
