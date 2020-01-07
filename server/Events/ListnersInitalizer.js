const subscriptionaccountslistner = require('./Listeners/SubscriptionscreatedListener')

exports.InitializeListeners = () => {
    subscriptionaccountslistner.SubscriptionAccountCreated();
}