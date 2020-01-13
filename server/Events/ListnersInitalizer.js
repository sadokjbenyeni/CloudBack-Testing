const subscriptionaccountslistner = require('./Listeners/SubscriptionscreatedListener')

exports.InitializeListeners = () => {
    subscriptionaccountslistner.SubscriptionAccountCreated();
    console.log("RabbitMQ listeners initialized");
}