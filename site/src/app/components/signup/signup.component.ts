import { Component, OnInit, ViewChild, HostBinding } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { CurrencyService } from '../../services/currency.service';
import { CountriesService } from '../../services/countries.service';
import { CompanytypesService } from '../../services/companytypes.service';
import { MatDialog } from '@angular/material';
import { TermsOfUseComponent } from '../terms-of-use/terms-of-use.component';
import { TermsService } from '../../services/terms.service';
import { Term } from '../../models/Terms';
import { LoginDialogComponent } from '../login/login-dialog/login-dialog.component';

export interface FormModel {
  captcha?: string;
}

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})

export class SignUpComponent implements OnInit {
  confirmation: string;
  isValidPwd: boolean;
  oldAddressBilling: { addressBilling: string; cityBilling: string; postalCodeBilling: string; countryBilling: string; };
  title: string;
  same: boolean;
  loadvat: string;
  message: string;
  coll: string;
  colg: string;
  key: string;
  pnl: string;
  user: object;
  page: string;
  payments: Array<object>;
  currencies: Array<object>;
  companyType: Array<object>;
  country: Array<object>;
  exist: boolean;
  checkrobot: boolean;
  checkv: boolean; // check VAT Number
  term: object

  acceptingcgu: false;


  public formModel: FormModel = {};

  constructor(
    private router: Router,
    private userService: UserService,
    private currencyService: CurrencyService,
    private countriesService: CountriesService,
    private companytypesService: CompanytypesService,
    private termService: TermsService,
    private matDialog: MatDialog
  ) {
  }

  // @ViewChild('utilisateurForm', { static: false })
  // private utilisateurForm: NgForm;

  ngOnInit() {
    this.page = this.router.url;
    this.key = '6Lcnsb0UAAAAAOqmrPXzJhOVbhcYZLrCwLngdjjb'; //key reCaptcha
    this.pnl = 'panel panel-primary';
    this.confirmation = '';
    this.isValidPwd = false;
    this.message = '';
    this.exist = false;
    this.checkrobot = true;
    this.checkv = false;
    this.loadvat = 'form-control ok';
    this.user = <object>{
      id: <string>'',
      firstname: <string>'',
      lastname: <string>'',
      email: <string>'',
      password: <string>'',
      job: <string>'',
      companyName: <string>'',
      companyType: <string>'',
      country: <string>'',
      address: <string>'',
      postalCode: <string>'',
      city: <string>'',
      countryBilling: <string>'',
      addressBilling: <string>'',
      postalCodeBilling: <string>'',
      cityBilling: <string>'',
      region: <string>'',
      phone: <string>'',
      website: <string>'',
      currency: <string>'',
      payment: <string>'',
      vat: <string>'',
      checkvat: <boolean>false,
      cgv: <boolean>false,
      cgu: [],
      commercial: <boolean>true
    };
    this.term = <object>{
      _id: <string>'',
      name: <string>'',
      description: <string>'',
      version: <string>''
    };
    this.title = 'Sign Up';
    if (this.page === '/account') {
      this.title = 'My Profile';
      this.getUser();
      this.getCurrency();
    }
    this.getCompanyType();
    this.getCountry();
    this.coll = 'col-lg-12';
    this.colg = 'col-lg-6';
    //getting last term of use
    this.termService.getLastTerm().subscribe(result => {
      this.term = result;
    });
  }


  resolved(captchaResponse: string) {
    if (captchaResponse) {
      this.checkrobot = false;
    } else {
      this.checkrobot = true;
    }
  }

  Signup() {
    if (this.confirmation === this.user['password']) {
      this.isValidPwd = false;
      this.userService.create(this.user).subscribe(data => {
        this.router.navigateByUrl("/on-boarding");
        this.matDialog.open(LoginDialogComponent, { data: { registration: "ok" } });
      },
        error => {
          console.error(error);
        });
    } else {
      this.isValidPwd = true;
    }
  }


  update() {
    this.userService.updateUser(this.user).subscribe(res => {
      this.message = res.message;
      let user = {};
      user['_id'] = this.user['_id']
      user['token'] = this.user['token']
      user['address'] = this.user['address']
      user['addressBilling'] = this.user['addressBilling']
      user['city'] = this.user['city']
      user['cityBilling'] = this.user['cityBilling']
      user['country'] = this.user['country']
      user['countryBilling'] = this.user['countryBilling']
      user['payment'] = this.user['payment']
      user['postalCode'] = this.user['postalCode']
      user['postalCodeBilling'] = this.user['postalCodeBilling']
      user['vat'] = this.user['vat']
      user['checkvat'] = this.user['checkvat']
      user['state'] = this.user['state']
      user['roleName'] = this.user['roleName']
      user['currency'] = this.user['currency']
      user['sameAddress'] = this.user['sameAddress']
      sessionStorage.setItem('user',  JSON.stringify(user));
    });
  }
  sameAddress() {
    if (this.user['sameAddress']) {
      this.user['addressBilling'] = this.user['address'];
      this.user['cityBilling'] = this.user['city'];
      this.user['postalCodeBilling'] = this.user['postalCode'];
      this.user['countryBilling'] = this.user['country'];
    }
    else {
      this.user['addressBilling'] = this.oldAddressBilling['addressBilling'];
      this.user['cityBilling'] = this.oldAddressBilling['cityBilling'];
      this.user['postalCodeBilling'] = this.oldAddressBilling['postalCodeBilling'];
      this.user['countryBilling'] = this.oldAddressBilling['countryBilling'];
    }
  }

  verifMail() {
    if (this.page === '/signup') {
      this.userService.verifmail({ email: this.user['email'] }).subscribe(resp => {
        this.exist = resp.valid;
      });
    }
  }

  getUser() {
    let id = JSON.parse(sessionStorage.getItem('user'))._id;
    this.userService.getCompte(id).subscribe(res => {
      this.user = res;
      this.user['id'] = id;
      if (!this.user['vat']) {
        this.user['vat'] = '';
      }
      // if(!this.user['checkvat']){ this.checkVat() };
      this.oldAddressBilling = {
        addressBilling: this.user['addressBilling'],
        cityBilling: this.user['cityBilling'],
        postalCodeBilling: this.user['postalCodeBilling'],
        countryBilling: this.user['countryBilling']
      }
    });
  }

  getCurrency() {
    this.currencyService.getCurrencies().subscribe(res => {
      this.currencies = res.currencies;
    });
  }

  getCompanyType() {
    this.companytypesService.getCompanytypes().subscribe(res => {
      this.companyType = res.companytypes;
    });
  }

  getCountry() {
    this.countriesService.getCountries().subscribe(res => {
      this.country = res.countries;
    });
  }
  GetTerms() {
    this.matDialog.open(TermsOfUseComponent, { disableClose: true, height: '90vh', maxHeight: '100vh', data: { terms: this.term, checked: this.acceptingcgu } }).afterClosed()
      .subscribe(reuslt => {
        this.acceptingcgu = reuslt;
      });
  }
  showOptions(event) {
    if (event.checked) {
      this.user['cgu'].push(this.term['version']);
    }
    else {
      this.user['cgu'].splice(this.user['cgu'].indexOf(this.term['version']), 1);
    }
  }
}

