using Akka.Actor;
using CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.Commands;
using CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.Queries;
using CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.ReadModels;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory
{
    public class SubscriptionProcessingHistoryRepository : ReceiveActor
    {
        public SubscriptionStatus SubscriptionStatus { get; private set; }
        public int SubscriptionProcessing { get; private set; }

        public SubscriptionProcessingHistoryRepository()
        {
            SubscriptionProcessing = 0;
            SubscriptionStatus = new SubscriptionStatus("");

            Receive<AddSubscriptionProcessingHistoryCommand>(Handle);
            Receive<GetSubscriptionProcessingHistoryQuery>(Handle);
        }

        private bool Handle(AddSubscriptionProcessingHistoryCommand command)
        {
            SubscriptionStatus = command.SubscriptionStatus;
            SubscriptionProcessing++;
            return true;
        }

        private bool Handle(GetSubscriptionProcessingHistoryQuery query)
        {
            var readModel = new SubscriptionProcessingHistoryReadModel(SubscriptionStatus, SubscriptionProcessing);
            Sender.Tell(readModel);
            return true;
        }
    }
}
