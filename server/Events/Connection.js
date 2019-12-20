var amqp = require('amqplib/callback_api');
var ch = null;

exports.Connect = async () =>
    amqp.connect(process.env.RABBITMQURL, function (errorconnect, connection) {
        if (errorconnect) {
            console.log("error occured while conecting to rmq");
            throw errorconnect;
        }
        connection.createChannel(function (errorchannel, channel) {
            if (errorchannel) {
                console.log("error occured while creating a channel to rmq");
                throw errorchannel;
            }
            console.log("connected to rabbitmq")
            ch = channel;
        });
    });

exports.PublishMessage = async function (exchange, routingKey, message) {
    await ch.publish(exchange, routingKey, encode(JSON.stringify(message)));
    console.log("event pushed to the exchange " + exchange + " with the routing key " + routingKey);

}
const encode = (message) => {
    return Buffer.from(message)
}