using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudBacktesting.SubscriptionService.Infra.Services
{
    public interface IMailNotificationService
    {
        Task Send(string sender, Receivers receviers, string subject, string message, CancellationToken cancellationToken = default);
    }

    public class Receivers
    {
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public IEnumerable<string> Bcc { get; set; }
    }
}
