const mongoose = require('mongoose');
const channel = require('../Connection')
const User = mongoose.model('User');
exports.SubscriptionAccountCreated = () => {
  channel.ConsumeMessage("SubscriptionAccountCreation",
    function (message) {
      
     var stringmessage=message.content;
      var messagebody=JSON.parse(stringmessage);
      console.log("message received from SubscriptionAccountCreation " + message.content.toString())
      User.update({ email: messagebody.email }, { $set: { subscriptionAccountId: messagebody.subscriptionAccountId } })
        .then(async () => {
          console.log("subscription account created");
        })
        .catch(err => {
          console.error(err);
        });
    })
}
