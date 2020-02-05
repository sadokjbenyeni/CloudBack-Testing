//Import dependencies
const express = require('express');
const path = require('path');
const http = require('http');
const bodyParser = require('body-parser');
const mongoose = require('mongoose');


//const cron = require('node-cron');
var resultEnv = false;
if (process.env.NODEJSENVIRONMENT != undefined) {
  console.log ("retrieving config file from environment variable "+  process.env.NODEJSENVIRONMENT )
  require('dotenv').config({ path: "environment/" + process.env.NODEJSENVIRONMENT + ".env" });
  resultEnv = true;
}
else {
  console.log ("retrieving config file from arguments")
  process.argv.forEach(function (item) {
    if (item.substr(0, 4) === "env=") {
      let environement = item.replace("env=", "");
      require('dotenv').config({ path: "environment/" + environement + ".env" });
      resultEnv = true;
    }
  });
}
if (resultEnv) {
  console.log("Environment variables has been loaded");
}
else {
  console.log("An error has been detected during the Environment variable loading");
  throw new Exception("Environment variables is not found.");
}
//Connect to mongoDB server
// const userdb = ''; � param�trer
// const passdb = ''; � param�trer
// mongoose.connect('mongodb://userdb:passdb@localhost:27017/histodataweb', {
//
// AZURE
//
//  var mongoClient = require("mongodb").MongoClient;
//  mongoClient.connect("mongodb://cloudbacktesting:Mj9Es9gbZO6xeN2PLsOhfZhMZLXCKYRnXuZZCEjVdkVUlruOPXguIGY4VkzmUCYBsdTNFCbSiNkJVt09zPdjoQ%3D%3D@cloudbacktesting.documents.azure.com:10255/?ssl=true", function (err, client) {
//     client.close();
//  });
//
console.log("the connectionstring is " + process.env.MONGODB_CONNECTIONSTRING);
mongoose.connect(process.env.MONGODB_CONNECTIONSTRING, {
  // useMongoClient: true,
  useNewUrlParser: true,
  /* other options */
});
mongoose.set('debug', true);

//Init express
const app = express();

//Passport
const passport = require('passport');
require('./server/config/passport')(passport); // pass passport for configuration

//Enable CORS
app.use(function (req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Methods", "GET, POST, DELETE, PUT");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
  next();
});
//Cookie and session
const cookieParser = require('cookie-parser');
const session = require('express-session');
app.use(session({
  secret: '#Les défits valent d\'être relevé%si§et seulement si ils¤ont quelques_choses à apporter à l\'humanité !'
}));
app.use(cookieParser());
app.use(passport.initialize());
app.use(passport.session());

//Enable bodyParser
app.use(bodyParser.json({ limit: '10mb' }));
app.use(bodyParser.urlencoded({ extended: true }));

//Require the models
require('./server/models/config');
require('./server/models/asset');
require('./server/models/exchange');
require('./server/models/user');
require('./server/models/role');
require('./server/models/order');
require('./server/models/pool');
require('./server/models/payment');
require('./server/models/currency');
require('./server/models/countrie');
require('./server/models/companytype');
require('./server/models/payment');
require('./server/models/termsofuse');
require('./server/models/termsofsale')
require('./server/models/termsofuse');



//Get our API routes
const api = require('./server/api/');

//Set API routes
app.use('/api', api);



// BEGIN CRON
// A exporter de ce fichier pour plus de souplesse

// const cronCurrency = cron.schedule('30 15 * * *', function(){
//   request.post({
//       headers: {'content-type' : 'application/x-www-form-urlencoded'},
//       url: 'http://localhost:9095/api/currency'
//     }, (err, r, body) => {
//   });
// });
// cronCurrency.start();
// test.destroy();


// const cronAutovalidationPVF = cron.schedule('30 15 * * *', function(){
//   request.post({
//       headers: {'content-type' : 'application/x-www-form-urlencoded'},
//       url: 'http://localhost:9095/api/order/autovalidation'
//     }, (err, r, body) => {
//   });
// });
// cronAutovalidationPVF.start();
// cronAutovalidationPVF.destroy();

// END CRON

//Static path to dist
// app.use(express.static(path.join(__dirname, 'site/dist')));
// app.use('/files', express.static(path.join(__dirname, 'files')));
// app.use('/cmd', express.static(path.join(__dirname, 'files/command')));
// app.use('/iv', express.static(path.join(__dirname, 'files/invoice')));
// app.use('/loadfile', express.static('/histoondemand/mapr_exports/'));
// app.use('/help/dataguide', express.static(path.join(__dirname, 'dataguide/')));

//Catch all other routes and return to the index file
app.get('/', (req, res) => {
    res.status(200).send("");
  // res.sendFile(path.join(__dirname, 'site/dist/index.html'));
})
console.log(" port number is " + process.env.PORT);
//Get environment port or use 9095
const port = process.env.PORT || '9095';
app.set('port', port);



//Create HTTP server.
const server = http.createServer(app);

//initalizing rmq event listeners
// var listenerinitialisers = new eventsgetter();
// listenerinitialisers.on('connected', function () {
//   console.log("connected")
// })



//connecting to rabbitmq
// the connection declaration must be set after the database.connect to load schema
const Connection = require('./server/Events/Connection')
try
{
Connection.Connect();
}
catch (error)
{
  console.log (error)
}
//Listen on port
server.listen(port, () => console.log(`API running on localhost:${port}`));
