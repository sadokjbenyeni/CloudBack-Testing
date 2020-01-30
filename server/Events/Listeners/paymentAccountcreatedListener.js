const mongoose = require('mongoose');
const channel = require('../Connection')
const User = mongoose.model('User');
exports.PaymentAccountCreated = () => {
  channel.ConsumeMessage("PaymentAccountCreation",
    function (message) {
      
     var stringmessage=message.content;
      var messagebody=JSON.parse(stringmessage);
      console.log("message received from PaymentAccountCreation " + message.content.toString())
      User.update({ email: messagebody.email }, { $set: { paymentAccountId: messagebody.paymentAccountId } })
        .then(async () => {
          console.log("payment account created");
        })
        .catch(err => {
          console.error(err);
        });
    })
}
