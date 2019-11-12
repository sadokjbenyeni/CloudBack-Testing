using Akka.Actor;
using CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.Commands;
using CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.Queries;
using CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory.ReadModels;
using CloudBacktesting.SubscriptionService.Domain.Model.ValueObjects;

namespace CloudBacktesting.SubscriptionService.Domain.Model.Repositories.SubscriptionProcessingHistory
{
    public class SubscriptionProcessingHistoryRepository : ReceiveActor
    {
        public SubscriptionStatus SubscriptionStatus { get; private set; }
        public SubscriptionUser SubscriptionUser { get; private set; }
        public SubscriptionType SubscriptionType { get; private set; }
        public SubscriptionDate SubscriptionDate { get; private set;  }
        public int SubscriptionProcessing { get; private set; }

        public SubscriptionProcessingHistoryRepository()
        {
            SubscriptionProcessing = 0;
            SubscriptionStatus = new SubscriptionStatus("");
            SubscriptionUser = new SubscriptionUser("");
            SubscriptionType = new SubscriptionType("");
            SubscriptionDate = new SubscriptionDate(System.DateTime.Now);


            Receive<AddSubscriptionProcessingHistoryCommand>(Handle);
            Receive<GetSubscriptionProcessingHistoryQuery>(Handle);
        }

        private bool Handle(AddSubscriptionProcessingHistoryCommand command)
        {
            SubscriptionStatus = command.SubscriptionStatus;
            SubscriptionUser = command.SubscriptionUser;
            SubscriptionType = command.SubscriptionType;
            SubscriptionDate = command.SubscriptionDate;

            SubscriptionProcessing++;
            return true;
        }

        private bool Handle(GetSubscriptionProcessingHistoryQuery query)
        {
            var readModel = new SubscriptionProcessingHistoryReadModel(SubscriptionStatus, SubscriptionUser, SubscriptionType, SubscriptionDate, SubscriptionProcessing);
            Sender.Tell(readModel);
            return true;
        }
    }
}
