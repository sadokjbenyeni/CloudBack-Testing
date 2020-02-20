const router = require('express').Router();
const sgMail = require('@sendgrid/mail');
const mongoose = require('mongoose');
const User = mongoose.model('User');

sgMail.setApiKey("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");
router.post('/inscription', (req, res, next) => {
  // sgMail.setApiKey("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");

  if (sendActivationMail(req.body.email, req.body.token))
    return res.status(200).json({ mail: true });
  else {
    console.log("error in sending mail ")
    return res.status(500).json({ mail: true });
  }
})

router.post('/activation', (req, res, next) => {
  let login = '';
  let password = '';
  let link = '';
  // if(URLS.indexOf(req.headers.referer) !== -1){
  let mailOptions = {
    from: 'no-reply@quanthouse.com',
    to: req.body.email, //req.body.user,
    subject: 'Account Activation',
    text: `Hello,
      Thank you for choosing QH’s On Demand Historical Data product!
      Your account has been successfully created. Below are your login credentials.
      Login: ` + login + `
      Password: ` + password + `
      To manage your profile please go to: ` + link + `

      The Quanthouse team`,

    html: `Hello,<br><br>
      Thank you for choosing QH’s On Demand Historical Data product!
      Your account has been successfully created. Below are your login credentials.
      Login: ` + login + `<br>
      Password: ` + password + `<br><br>
      To manage your profile please go to: ` + link + `
      <br><br>
      <b>The Quanthouse team</b>`
  };
  smtpTransport.sendMail(mailOptions, (error, info) => {
    if (error) {
      return console.log(error);
    }
    return res.json({ mail: true }).statusCode(200);
  });

});

router.post('/activated', (req, res, next) => {
  let login = '';
  let password = '';
  let link = '';
  let mailOptions = {
    from: 'no-reply@quanthouse.com',
    to: req.body.email,
    subject: 'Your Account has been validated',
    text: `Hello,
      Thank you for choosing QH’s On Demand Historical Data product!
      Your account has been successfully created. Below are your login credentials.
      Login: ` + login + `
      Password: ` + password + `
      To manage your profile please go to: ` + link + `

      The Quanthouse team`,

    html: `Hello,<br><br>
      Thank you for choosing QH’s On Demand Historical Data product!
      Your account has been successfully created. Below are your login credentials.
      Login: ` + login + `<br>
      Password: ` + password + `<br><br>
      To manage your profile please go to: ` + link + `
      <br><br>
      <b>The Quanthouse team</b>`
  };
  smtpTransport.sendMail(mailOptions, (error, info) => {
    if (error) {
      return console.log(error);
    }
    return res.json({ mail: true }).statusCode(200);
  });
});

router.post('/mdp', (req, res) => {
  if (req.body.email == undefined) {
    console.log("no email provided");
    return res.status(400).send({ sent: false, message: "no Email provided" })
  }
  console.log("sneding email of password reset to " + req.body.email)
  User.findOne({ email: req.body.email }, { token: true })
    .then((u) => {
      let mailOptions = {
        from: 'no-reply@quanthouse.com',
        to: req.body.email,
        subject: 'Password Initialization',
        text: `Hello,

    To reinitialize your password, please click on the following link: `+ process.env.DOMAIN + `/mdp/` + u.token + `
    If clicking the above link does not work, you can copy and paste the URL in a new browser window.
    If you have received this email by error, you do not need to take any action. Your password will remain unchanged.

    The Quanthouse team`,

        html: `Hello,<br><br>
    To reinitialize your password, please click on the following link: `+ process.env.DOMAIN + `/mdp/` + u.token + `<br>
    If clicking the above link does not work, you can copy and paste the URL in a new browser window.<br>
    If you have received this email by error, you do not need to take any action. Your password will remain unchanged.<br><br>
    <b>The Quanthouse team</b>`
      };
      try {
        sgMail.send(mailOptions);
        return res.status(200).json({ mail: true });
      } catch (error) {
        console.log(error);
        return res.status(d).json({ mail: true });
      }
    });

});

const sendActivationMail = function (email, token) {
  console.log("sending activation mail from the webapi");
  const msg = {
    to: email,
    from: 'no-replay@cloudbacktesting.com',
    subject: 'Confirm your email',
    text: `Hello,

    
    To validate your email address and activate your account, please click on the following link:
    `+ process.env.DOMAIN + `/activation/` + token +
      `If clicking the above link does not work, you can copy and paste the URL in a new browser window.

    If you have received this email by error, you do not need to take any action. The account will not be activated and you will not receive any further emails.


    The Quanthouse team`,
    html: `Hello,<br><br>
       To validate your email address and activate your account, please click on the following link:
       <a href="`+ process.env.DOMAIN + `/activation/` + token + `">Activation of the HistodataWeb account</a><br>
       If clicking the above link does not work, you can copy and paste the URL in a new browser window.<br><br>
       If you have received this email by error, you do not need to take any action. The account will not be activated and you will not receive any further emails.
       <br><br>
       <b>The Quanthouse team</b>`,
  };
  try {
    sgMail.send(msg);
    console.log("Mail sent!");
    return true;
  }
  catch
  {
    return false
  }
}


module.exports = router;
module.exports.sendActivationMail = sendActivationMail;
