const mongoose = require('mongoose');
const User = mongoose.model('User');

// Access for Jobs
//exports.rtconfig = () => { const urls = [ "192.168.10.10" ]; }

// Domain of site
//exports.domain = () => { return "http://cloudbacktesting.quanthouse.com"; }
exports.domain=()=> process.env.DOMAIN;
// // IPs ElastcSearch
// exports.hostsES = () => { return ['10.0.10.218:9200', '10.0.10.190:9200', '10.0.10.234:9200']; }

// Param SMTP
exports.smtpconf = () => {
    return {
        host: 'quanthouse-com.mail.protection.outlook.com',
        port: 25,
        secure: false,
        tls: {rejectUnauthorized: false},
        debug:false
    };
}
exports.phrase = () => { return '14cb4a9235d7360d2911d6444aa69f02'; }

// // Domaine QuantFlow
// exports.apiqf = () => { return "http://10.1.0.6"; }

// Download file HoD
// exports.dnwfile = () => { return "http://gateway0X.hod.quanthouse.com"; }

// Link API Currency
exports.currency = () => { return "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"; }

// API Adyen ( System Payment )
exports.paymentVerify = () => { return "https://checkout-test.adyen.com/services/PaymentSetupAndVerification/v32/verify"; }
exports.paymentSetup = () => { return "https://checkout-test.adyen.com/services/PaymentSetupAndVerification/v32/setup"; }
exports.paymentKey = () => { return "AQElhmfuXNWTK0Qc+iSBp0UWk8y0fbhoaeebbhXE6Bp3O9OVDwjXkhDBXVsNvuR83LVYjEgiTGAH-72Ru3lyilp3K6oonHc6t8HPeGM63I61nZCG9LeyUyxk=-43mwGjBQkT2EbcVp"; }

// Access for front
exports.config = () => {
    const herbergement = this.domain();
    const urls = [
        herbergement + "/admin/configuration",
        herbergement + "/admin/users",
        herbergement + "/admin/profil/",
        herbergement + "/admin/role",
        herbergement + "/home",
        herbergement + "/login",
        herbergement + "/pwd",
        herbergement + "/register",
        herbergement + "/confirm",
        herbergement + "/contact",
        herbergement + "/active",
        herbergement + "/mdpoublie",
        herbergement + "/users",
    ];
    return urls;
}
exports.admin = ()=>{
	let emailAdm = [];
	User.find({type:"admin"},{_id:false, email:true})
	.then((adm)=>{
		adm.forEach(function(e) {
			emailAdm.push(e.email);
		});
	});
	
	return emailAdm;
}
