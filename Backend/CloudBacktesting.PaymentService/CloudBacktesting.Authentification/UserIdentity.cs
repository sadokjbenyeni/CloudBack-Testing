using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBacktesting.AuthentificationService.WebAPI.Entities
{
    public class UserIdentity
    {
        public string Login { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }
        public IReadOnlyDictionary<string, string> Additionals { get; }
    }
}
