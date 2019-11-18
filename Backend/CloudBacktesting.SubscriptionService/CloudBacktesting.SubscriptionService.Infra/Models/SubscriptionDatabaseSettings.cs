using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.SubscriptionService.Infra.Models
{
    public class SubscriptionDatabaseSettings : ISubscriptionDatabaseSettings
    {
        public string SubscriptionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISubscriptionDatabaseSettings
    {
        string SubscriptionCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
