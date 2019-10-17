const app = require('express')();
const router = require('express').Router();
const mongoose = require('mongoose');
const Console  = require('console');
const Terms = mongoose.model('usageterm');

const config = require('../config/config.js');

router.get('/', (req, res) => {
    Terms.find()
    .then((terms) => {
        if (!terms) { return res.sendStatus(404); }
        Console.log(terms);
        return res.status(200).json({terms: terms});
    });
});
router.get('/lastterm', (req, res) => {
    Terms.find()
    .then((terms) => {
        if (!terms) { return res.sendStatus(200); }
        return res.status(200).send(terms[terms.length-1])
    });
});
module.exports = router;