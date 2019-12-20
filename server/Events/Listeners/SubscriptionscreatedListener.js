const EventEmitter = require('events');
const Connection = require('../Connection')
class MyEmmitter extends EventEmitter {}
exports.wait()
channel.consume(subscriptionaccountqueue,
    function (message) {
      console.log("message received : ", message.content.toString())
    }, { noAck: true })