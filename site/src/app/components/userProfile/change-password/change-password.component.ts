import { Component, OnInit, Input } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  oldpassword: string
  newpassword: string;
  newvalidationpassword: string;
  message: boolean = false;
  passwordform: FormGroup
  constructor(
    public userservice: UserService,
    public route: ActivatedRoute,
    public router: Router,
    private formBuilder: FormBuilder,
    private snackbar: MatSnackBar
  ) {

  }

  ngOnInit() {
    //form validators
    this.passwordform = this.formBuilder.group({
      oldpasswordctl: ['', Validators.required],
      newpasswordctl: ['', Validators.required],
      newvalidationpasswordctl: new FormControl('', [(control) => {
        return !control.value ? { 'required': true } : control.value != this.passwordform.controls['newpasswordctl'].value ? { incorrect: true } : null;
      }])
    });

  }

  changePassword() {

    if (this.passwordform.valid) {
      this.userservice.changepwd(this.oldpassword, this.newpassword)
        .subscribe(() => {
          this.snackbar.open("Password Successfully updated", "", { duration: 3000 });
        },
          error => {
            if (error.status == 400) {
              this.passwordform.controls['oldpasswordctl'].setErrors({ 'incorrect': true })
            }
          });
    }
  }

}
