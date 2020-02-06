const router = require('express').Router();
const mongoose = require('mongoose');
const usageTerms = mongoose.model('usageterm');
const saleTerms = mongoose.model('saleterm');

router.get('/lastusgeterm', (req, res) => {
    usageTerms.find()
    .then((terms) => {
        if (!terms) { return res.sendStatus(200); }
        return res.status(200).send(terms[terms.length-1])
    });
});
router.get('/lastsaleterm', (req, res) => {
    saleTerms.find()
    .then((terms) => {
        if (!terms) { return res.sendStatus(200); }
        return res.status(200).send(terms[terms.length-1])
    });
});
module.exports = router;