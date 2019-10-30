const mongoose = require('mongoose');

let termsofuseSchema = new mongoose.Schema({
    id: { type: String, maxlength: 3 },
    name: { type: String, maxlength: 200 },
    version: { type: String, default: "0" }
});


termsofuseSchema.methods.AllTerms = function () {
    return {
        id: this.id,
        name: this.name,
        description: this.description,
        version: this.version
    }
};

termsofuseSchema.methods.findById = function (id, cb) {
    return this.find({ _id: Object(id) }, cb);
};
mongoose.model('usageterm', termsofuseSchema);