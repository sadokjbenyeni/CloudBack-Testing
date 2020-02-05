const subscriptionaccountslistner = require('./Listeners/SubscriptionscreatedListener')
const paymentaccountslistner = require('./Listeners/paymentAccountcreatedListener')


exports.InitializeListeners = () => {
    subscriptionaccountslistner.SubscriptionAccountCreated();
    paymentaccountslistner.PaymentAccountCreated();
    console.log("RabbitMQ listeners initialized");
}