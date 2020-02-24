import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { CountriesService } from '../../services/countries.service';
import { CompanytypesService } from '../../services/companytypes.service';
import { MatDialog } from '@angular/material';
import { TermsOfUseComponent } from '../terms-of-use/terms-of-use.component';
import { TermsService } from '../../services/terms.service';
import { LoginDialogComponent } from '../login/login-dialog/login-dialog.component';
import { User } from '../../models/User';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { createUrlResolverWithoutPackagePrefix } from '@angular/compiler';

export interface FormModel {
  captcha?: string;
}

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})

export class SignUpComponent implements OnInit {
  SignupFrmGroup: FormGroup;
  key: string;
  user: User;
  page: string;
  companyType: Array<object>;
  country: Array<object>;
  exist: boolean;
  checkrobot: boolean;
  term: object

  acceptingcgu: false;


  public formModel: FormModel = {};
  recaptchaerrormessage: boolean;

  constructor(
    private formbuilder: FormBuilder,
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
    // this.emailformcontrol.valueChanges.debounceTime(500).subscribe(
    //   () => this.verifMail()
    // )
    this.SignupFrmGroup = this.formbuilder.group(
      {
        firstnamectl: ['', Validators.required],
        lastnamectl: ['', Validators.required],
        jobctl: ['', Validators.required],
        phonectl: ['', Validators.pattern("[0-9]{9,16}")],

        emailctl: ['', [Validators.required, Validators.email]],
        passwordctl: ['', Validators.required],
        // confirmationpasswordctl: ['', Validators.required],
        confirmationpasswordctl: new FormControl('', [(control) => {
          return !control.value ? { 'required': true } : control.value != this.SignupFrmGroup.controls['passwordctl'].value ? { incorrect: true } : null;
        }]),
        companynamectl: ['', Validators.required],
        companytypectl: [''],
        countryctl: ['', Validators.required],
        addressctl: [''],
        postalcodectl: [''],
        cityctl: [''],
        regionctl: [''],
        websitectl: [''],
        cguctl: new FormControl('', [(control) => {
          return !control.value ? { 'required': true } : null;
        }]
        )

      }
    )
    this.key = '6Lcnsb0UAAAAAOqmrPXzJhOVbhcYZLrCwLngdjjb'; //key reCaptcha
    this.exist = false;
    this.checkrobot = false;
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
      this.checkrobot = true;
      this.recaptchaerrormessage = false
    } else {
      this.checkrobot = false;
    }
  }

  Signup() {
    if (!this.checkrobot) {
      this.recaptchaerrormessage = true;
      return;
    }
    if (this.SignupFrmGroup.valid) {
      this.createUser();
      this.userService.create(this.user).subscribe(data => {
        if (data["error"] == "email already exists") {
          this.SignupFrmGroup.controls["emailctl"].setErrors({ 'incorrect': true })

          return;
        }
        this.router.navigateByUrl("/on-boarding");
        this.matDialog.open(LoginDialogComponent, { data: { registration: "ok" } });
      },
        error => {
          console.error(error);
        });
    }
  }
  createUser() {
    this.user.firstname = this.SignupFrmGroup.controls["firstnamectl"].value;
    this.user.lastname = this.SignupFrmGroup.controls["lastnamectl"].value;
    this.user.job = this.SignupFrmGroup.controls["jobctl"].value;
    this.user.phone = this.SignupFrmGroup.controls["phonectl"].value;
    this.user.email = this.SignupFrmGroup.controls["emailctl"].value;
    this.user.password = this.SignupFrmGroup.controls["passwordctl"].value;
    this.user.companyName = this.SignupFrmGroup.controls["companynamectl"].value;
    this.user.companyType = this.SignupFrmGroup.controls["companytypectl"].value;
    this.user.country = this.SignupFrmGroup.controls["countryctl"].value;
    this.user.address = this.SignupFrmGroup.controls["addressctl"].value;
    this.user.postalCode = this.SignupFrmGroup.controls["postalcodectl"].value;
    this.user.city = this.SignupFrmGroup.controls["cityctl"].value;
    this.user.region = this.SignupFrmGroup.controls['regionctl'].value;
    this.user.website = this.SignupFrmGroup.controls['websitectl'].value;
  }



  verifMail() {
    if (this.user.email != null && this.user.email != '') {
      this.userService.verifmail({ email: this.user['email'] }).subscribe(resp => {
        this.exist = resp.valid;
      });
    }
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

