import { Component, OnInit, ViewChild, HostBinding } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { CountriesService } from '../../services/countries.service';
import { CompanytypesService } from '../../services/companytypes.service';
import { MatDialog } from '@angular/material';
import { TermsOfUseComponent } from '../terms-of-use/terms-of-use.component';
import { TermsService } from '../../services/terms.service';
import { LoginDialogComponent } from '../login/login-dialog/login-dialog.component';
import { User } from '../../models/User';

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
  coll: string;
  colg: string;
  key: string;
  pnl: string;
  user: User;
  page: string;
  companyType: Array<object>;
  country: Array<object>;
  exist: boolean;
  checkrobot: boolean;
  term: object

  acceptingcgu: false;


  public formModel: FormModel = {};

  constructor(
    private router: Router,
    private userService: UserService,
    private countriesService: CountriesService,
    private companytypesService: CompanytypesService,
    private termService: TermsService,
    private matDialog: MatDialog
  ) {
  }

  // @ViewChild('utilisateurForm', { static: false })
  // private utilisateurForm: NgForm;

  ngOnInit() {
    this.key = '6Lcnsb0UAAAAAOqmrPXzJhOVbhcYZLrCwLngdjjb'; //key reCaptcha
    this.confirmation = '';
    this.isValidPwd = false;
    this.exist = false;
    this.checkrobot = true;
    this.user = new User();
    this.term = <object>{
      _id: <string>'',
      name: <string>'',
      description: <string>'',
      version: <string>''
    };
    this.getCompanyType();
    this.getCountry();
    //getting last term of use
    this.termService.getLastUsageTerm().subscribe(result => {
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

  verifMail() {
      this.userService.verifmail({ email: this.user['email'] }).subscribe(resp => {
        this.exist = resp.valid;
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

