const router = require('express').Router();

router.use('/user', require('./user'));
router.use('/countries', require('./countries'));
router.use('/terms', require('./usageterms'));
router.use('/companytype', require('./companytype'));
router.use('/mail', require('./mailer'));

module.exports = router;