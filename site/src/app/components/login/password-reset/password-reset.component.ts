import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { nullSafeIsEquivalent } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {
  password: string;
  validationpassword: string;
  message: boolean = false;
  token: string;
  passwordform: FormGroup
  constructor(
    public userservice: UserService,
    public route: ActivatedRoute,
    public router: Router,
    private formBuilder: FormBuilder
  ) {

  }

  ngOnInit() {
    //form validators
    this.passwordform = this.formBuilder.group({
      passwordctl: ['', Validators.required],
      validationpasswordctl: ['', Validators.required],
    });
    //get token from url
    this.route.params.subscribe(event => {
      if (event.token == null) {
        this.router.navigateByUrl('/on-boarding');
      }
      this.token = event.token;
    })
  }

  changePassword() {
    if (this.password != this.validationpassword) {
      this.passwordform.controls['passwordctl'].setErrors({ 'incorrect': true })
      this.passwordform.controls['validationpasswordctl'].setErrors({ 'incorrect': true })
      return;
    }
    if (this.passwordform.valid) {
      this.userservice.resetpwd({ pwd: this.password, token: this.token }).subscribe(result => {
        this.message = true;
        setTimeout(() => {
          this.router.navigateByUrl('/login');
        }, 3000);
      });
    }
  }

}
