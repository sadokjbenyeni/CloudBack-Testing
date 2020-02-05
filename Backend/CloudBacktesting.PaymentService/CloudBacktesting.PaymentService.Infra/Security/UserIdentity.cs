using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.Infra.Security
{

    public class UserIdentity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IReadOnlyDictionary<string, string> Additionals { get; set; }
    }
}
