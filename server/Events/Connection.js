var amqp = require('amqplib/callback_api');
var initializer = require('./ListnersInitalizer')
var EventEmitter = require('events').EventEmitter;
var mongoose = require('mongoose');
var ch = null;

var rabbitmqcreation = new EventEmitter()
rabbitmqcreation.on("connected", () => {
    console.log("connecting successfully initializing listeners");
        initializer.InitializeListeners();
})
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
            console.log("connected to rabbitmq throwing 'connection with rmq established' event")
            ch = channel;
            rabbitmqcreation.emit('connected');
        });
    });

exports.PublishMessage = async function (exchange, routingKey, message) {
    await ch.publish(exchange, routingKey, encode(JSON.stringify(message)));
    console.log("event pushed to the exchange " + exchange + " with the routing key " + routingKey);

}
exports.ConsumeMessage = function (queuename, callback, noAck = true) {
    if (typeof callback != "function") {
        console.log("property is not a callback");
    }
    else {
        ch.consume(queuename,
            (message) => {
                callback(message);
            }, { noAck: noAck });

    }
}
const encode = (message) => {
    return Buffer.from(message)
}