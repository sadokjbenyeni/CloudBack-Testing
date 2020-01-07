const mongoose = require('mongoose');

let termsofsaleSchema = new mongoose.Schema({
    id: { type: String, maxlength: 3 },
    name: { type: String, maxlength: 200 },
    version: { type: String, default: "0" }
});


termsofsaleSchema.methods.AllTerms = function () {
    return {
        id: this.id,
        name: this.name,
        description: this.description,
        version: this.version
    }
};

termsofsaleSchema.methods.findById = function (id, cb) {
    return this.find({ _id: Object(id) }, cb);
};
mongoose.model('saleterm', termsofsaleSchema);