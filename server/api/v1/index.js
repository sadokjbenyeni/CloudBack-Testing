const router = require('express').Router();

router.use('/user', require('./user'));
router.use('/role', require('./role'));
router.use('/countries', require('./countries'));
router.use('/terms', require('./usageterms'));
router.use('/companytype', require('./companytype'));
router.use('/verifyvat', require('./vat'));
router.use('/payment', require('./payment'));
router.use('/mail', require('./mailer'));
router.use('/order', require('./order'));
router.use('/upload', require('./upload'));
router.use('/cmd', require('./todo'));
router.use('/currency', require('./currency'));
router.use('/config', require('./config'));

module.exports = router;