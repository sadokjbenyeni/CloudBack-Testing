using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using EventFlow.ReadStores;
using System.Collections.Generic;

namespace CloudBacktesting.SubscriptionService.WebAPI.Models.Request.Client.SubscriptionRequestDto
{
    public class SubscriptionRequestListItem
    {
        public string Status { get; set; }
        public string Subscriber { get; set; }
        public string Type { get; set; }
    }

    public class GetSubscriptionRequestsAdmin : IQuery<List<SubscriptionRequestListItem>>, IReadModel, IMongoDbReadModel
    {
        public string Id { get; set; }
        public long? Version { get; set; }
    }
}
