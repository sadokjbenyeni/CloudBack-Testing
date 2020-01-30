const channel = require('../Connection')
exports.AccountActivation = async function (useremail) {
    await channel.PublishMessage("activation", "AccountActivated", { email: useremail });
    console.log("accountActivationn event pushed to the exchange Signup with the routing key AccountActivation");
}
