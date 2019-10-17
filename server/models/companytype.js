//Configuration MODEL
 
const mongoose = require('mongoose');

let companytypeSchema = new mongoose.Schema({
  id: { type: String},
  name: { type: String, maxlength: 200 }
}, { timestamps: true });

companytypeSchema.methods.AllCompanytype = function () {
  return {
    id: this.id,
    name: this.name
  }
};

companytypeSchema.methods.findById = function(id, cb) {
 return this.find({ _id: Object(id) }, cb);
};

mongoose.model('Companytype', companytypeSchema);