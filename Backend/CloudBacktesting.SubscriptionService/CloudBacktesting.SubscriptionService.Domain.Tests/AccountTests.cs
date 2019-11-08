using Akka.TestKit.Xunit2;
using Akkatecture.TestFixture.Extensions;
using Xunit;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.Commands;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.ValueObjects;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount;
using CloudBacktesting.SubscriptionService.Domain.Model.SubscriptionAccount.Events;

namespace CloudBacktesting.SubscriptionService.Domain.Tests
{
    public class AccountTests : TestKit
    {
        public AccountTests() : base(string.Empty)
        {

        }

        [Fact]
        public void should_emit_subscription_open_when_open_new_subscription()
        {
            var accountId = SubscriptionAccountId.New;
            var state = new SubscriptionStatus("Creating");

            this.FixtureFor<SubscriptionAccount, SubscriptionAccountId>(accountId)
                .GivenNothing()
                .When(new CreateSubscriptionCommand(accountId, state))
                .ThenExpect<SubscriptionCreatedEvent>(x => x.SubscriptionState == state);
        }
    }
}
