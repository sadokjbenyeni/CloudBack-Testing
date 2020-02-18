// const app = require('express')();
const router = require('express').Router();
const mongoose = require('mongoose');
const crypto = require("crypto");
const eventpublisher = require('../../Events/Publishers/AccountActiveEventPublisher')
const User = mongoose.model('User');
const config = require('../../config/config.js');
const mailer = require('./mailer.js');
// const Request = require('request');
const URLS = config.config();
// const admin = config.admin();
// const domain = config.domain();
const PHRASE = config.phrase();
// const DNLFILE = config.dnwfile();
const algorithm = 'aes256';

// router.param('user', function (req, res, next) {
//     if (!id.match(/^[0-9a-fA-F]{24}$/)) {
//         return next();

//         return res.sendStatus(422);
//     }
// });

router.get('/', (req, res) => {
    if (URLS.indexOf(req.headers.referer) !== -1) {
        User.find().sort({ "firstname": 1, "lastname": 1 })
            .then((users) => {
                if (!users) { return res.sendStatus(404); }
                return res.status(200).json({ users: users });
            });
    }
    else {
        return res.sendStatus(404);
    }
});

router.get('/count/', (req, res) => {
    if (URLS.indexOf(req.headers.referer) !== -1) {
        User.count()
            .then((count) => {
                return res.status(200).json({ nb: count });
            });
    }
    else {
        return res.sendStatus(404);
    }
});

router.get('/test/:token/:id/:file', (req, res) => {
    let valid = false;
    // http://10.1.0.5:3000/loadfile/3-qh-20180522-1/2017-1-10_1027_L1.csv.gz
    User.findOne({ token: req.params.token }, { _id: true })
        .then((u) => {
            if (u) {
                let idUser = JSON.parse(JSON.stringify(u._id));
                // Order.findOne({idUser: idUser, 'products.id_undercmd': req.params.id}, {'products.$.id_undercmd':true, _id:false})
                Order.findOne({ idUser: idUser, 'products.id_undercmd': req.params.id.split('|')[0] })
                    .select({ 'products.$.id_undercmd': 1, '_id': false })
                    .then(o => {
                        if (o) {
                            o.products[0].links.forEach(lk => {
                                if (lk.status === 'active') {
                                    lk.links.forEach(link => {
                                        let rgx = RegExp(req.params.file);
                                        valid += rgx.test(link.link);
                                    })
                                }
                            })
                        } else {
                            res.status(404).end();
                        }
                    })
                    .then(() => {
                        if (valid) {
                            res.download('/mapr/client_exports/' + req.params.id + '/' + req.params.file);
                        } else {
                            res.status(404).end();
                        }
                    });
            } else {
                res.status(404).end();
            }
        })
        .catch(err => {
            res.status(500).end();
        });
});

router.get('/cpt/', (req, res) => {
    User.findOne({ nbSession: 1 }, { _id: false, count: true })
        .then((nb) => {
            return res.status(200).json(nb);
        });
});
router.get('/informations', (req, res) => {
    const email = getEmailfromheader(req);
    if (email == undefined) {
        return res.sendStatus(401)
    }
    User.findOne({ email: Object(email) }, { password: false })
        .then((val) => {
            if (val == undefined) {
                return res.sendStatus(204)
            }
            return res.status(200).json(val)
        })
});
router.get('/info', (req, res) => {
    if (req.headers["authorization"] == undefined) {
        return res.sendStatus(204);
    }
    User.findOne({ token: Object(req.headers["authorization"]) }, { password: false })
        .then((val) => {
            if (val == undefined) {
                return res.sendStatus(204)
            }
            return res.status(200).json(val)
        })
});
router.get('/:user', (req, res) => {
    // let test = req.headers.referer.replace(idd, "");
    // if(URLS.indexOf(test) !== -1){
    User.findOne({ _id: Object(req.params.user) }, { password: false, token: false })
        .then((user) => {
            if (!user) { res.status(202).json({}) }
            return res.status(200).json(user);
        });
    // }
    // else{
    //     return res.sendStatus(404);
    // }
});

router.post('/info', (req, res) => {
    // let test = req.headers.referer.replace(idd, "");
    // if(URLS.indexOf(test) !== -1){
    let u = {};
    // u['_id'] = false;
    if (req.body.field !== 'password' && typeof req.body.field === 'string') { return res.sendStatus(404); }
    if (req.body.field && typeof req.body.field === 'string') {
        u[req.body.field] = true;
    }
    if (typeof req.body.field === 'object') {
        req.body.field.forEach(field => {
            u[field] = true;
        });
    }
    User.findOne({ token: req.body.token }, u)
        .then((user) => {
            if (!user) { res.status(202).json({}) }
            return res.status(200).json({ user: user });
        });
    // }
    // else{
    //     return res.sendStatus(404);
    // }
});

//Create Account User
router.post('/', (req, res) => {
    User.count()
        .then((count) => {
            let user = new User();
            let d = new Date();

            let concatoken = req.body.password + req.body.email + d;
            let pass = req.body.password;
            let cipher = crypto.createCipher(algorithm, pass);
            let crypted = cipher.update(PHRASE, 'utf8', 'hex');
            crypted += cipher.final('hex');
            user.password = crypted;
            cipher = crypto.createCipher(algorithm, concatoken);
            crypted = cipher.update(PHRASE, 'utf8', 'hex');
            crypted += cipher.final('hex');
            user.token = crypted;

            user.id = count + 1;
            user.email = req.body.email;
            user.lastname = req.body.lastname;
            user.firstname = req.body.firstname;
            user.job = req.body.job;
            user.companyName = req.body.companyName;
            user.companyType = req.body.companyType ? req.body.companyType : '';
            user.country = req.body.country ? req.body.country : '';
            user.address = req.body.address ? req.body.address : '';
            user.postalCode = req.body.postalCode ? req.body.postalCode : '';
            user.city = req.body.city ? req.body.city : '';
            user.region = req.body.region ? req.body.region : '';
            user.phone = req.body.phone ? req.body.phone : '';
            user.website = req.body.website ? req.body.website : '';
            user.cgu = req.body.cgu;
            user.save((err) => {
                if (err) return console.error(err);
                console.log("User successfully created : Sending mail to user ")
                if (mailer.sendActivationMail(user.email, user.token) == true) {
                    res.status(201).json({ account: true });
                } else {
                    res.sendStatus(503);
                }
            });
        });
});

router.post('/logout/', (req, res) => {
    email = getEmailfromheader(req);
    User.updateOne({ email: email  }, { $set: { islogin: false } })
        .then(() => {
            res.status(200).json({});
        })
        .catch((err) => {
            console.error(err);
        });
});
router.post('/check/', (req, res) => {
    let cipher = crypto.createCipher(algorithm, req.body.pwd);
    let crypted = cipher.update(PHRASE, 'utf8', 'hex');
    crypted += cipher.final('hex');

    User.findOne(
        { email: req.body.email, password: crypted },
        {
            roleName: true,
            token: true,
            lastname: true
        })
        .then((user) => {
            if (!user) { res.status(202).json({ user: false, message: 'Invalid Password or User Not Found' }) }
            if (user.state === 1) {
                User.updateOne({ email: req.body.email }, { $set: { islogin: true } })
                    .then(() => {
                        res.status(200).json({ user: user });
                    });
            } else {
                res.status(202).json({ message: 'Your account is not activated' })
            }
        });
});

router.post('/activation/', async (req, res) => {
    await User.findOne({ token: req.body.token }).then(async (result) => {
        if (result == undefined) {
            console.log("user not found");
            res.status(200).json({ message: "User Not Found" })
        }
        if (result.state == 0) {
            await User.update({ token: req.body.token }, { $set: { state: 1 } })
                .then(async () => {
                    console.log("account activated");
                    res.status(200).json({ message: "Your account is activated. You can connect" });
                    await eventpublisher.AccountActivation(result.email)
                })
                .catch(err => {
                    console.error(err);
                });
        }
        else {
            console.log("account already active");
            res.status(200).json({ message: "Your account is already active" })
        }
    })

});

router.post('/suspendre/', (req, res) => {
    User.update({ token: req.body.token }, { $set: { actif: -1 } })
        .then((user) => {
            if (!user) { res.status(200).json({}) }
            return res.status(200).json({ valid: true });
        });
});

router.post('/verifmail/', (req, res) => {
    // if(URLS.indexOf(req.headers.referer) !== -1){
    User.findOne({ email: req.body.email }, { _id: false })
        .then((user) => {
            if (!user) {
                return res.status(200).json({ valid: false, message: "This email does not exist" });
            }
            return res.status(200).json({ valid: true });
        });
});

router.delete('/:user', (req, res) => {
    req.user.remove()
        .then(() => {
            return res.sendStatus(200);
        })
});

router.put('/', (req, res) => {
    // if(URLS.indexOf(req.headers.referer) !== -1){
    let user = {};
    if (!req.body.nom == undefined && req.body.nom == undefined) {
        res.sendStatus(422);
    }

    user.firstname = req.body.firstname;
    user.lastname = req.body.lastname;
    user.job = req.body.job;
    user.companyName = req.body.companyName;
    user.companyType = req.body.companyType;
    user.website = req.body.website;
    user.address = req.body.address;
    user.postalCode = req.body.postalCode;
    user.city = req.body.city;
    user.region = req.body.region;
    user.idCountry = req.body.idCountry;
    user.country = req.body.country;
    user.phone = req.body.phone;
    user.sameAddress = req.body.sameAddress;
    user.roleName = req.body.roleName;
    user.role = req.body.role;

    const email = getEmailfromheader(req);
    if (email == undefined) {
        return res.sendStatus(401)
    }
    console.log(email);
    User.findOneAndUpdate({ email: email }, { $set: user })
        .then(() => {
            res.status(201).json({ message: "Your account has been updated" });
            return;
        })
        .catch((e) => {
            console.error(e);
        });
});
router.put('/resetpwd', (req, res) => {
    const email = getEmailfromheader(req)
    if (email == null) {
        return res.sendStatus(401);
    }
    if (!req.body.pwd && req.body.pwd == undefined) {
        res.sendStatus(422);
    }
    let cipher = crypto.createCipher(algorithm, req.body.pwd);
    let crypted = cipher.update(PHRASE, 'utf8', 'hex');
    crypted += cipher.final('hex');

    User.update({ email: email }, { $set: { password: crypted } })
        .then(() => {
            return res.sendStatus(200);
        })
});
var getEmailfromheader = function (req) {
    try {
        var rawtoken = Buffer.from(req.headers["authorization"].replace("Basic", ""), 'base64').toString('ascii');
        return JSON.parse(rawtoken)["Email"]
    }
    catch
    {
        return null;
    }
}
module.exports = router;