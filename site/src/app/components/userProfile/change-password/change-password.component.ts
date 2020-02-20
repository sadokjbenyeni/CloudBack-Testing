import { Component, OnInit, Input } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

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
    private formBuilder: FormBuilder
  ) {

  }

  ngOnInit() {
    //form validators
    this.passwordform = this.formBuilder.group({
      oldpasswordctl: ['', Validators.required],
      newpasswordctl: ['', Validators.required],
      newvalidationpasswordctl: ['', Validators.required],
    });

  }

  changePassword() {
    if (this.newpassword != this.newvalidationpassword) {
      this.passwordform.controls['newpasswordctl'].setErrors({ 'incorrect': true })
      this.passwordform.controls['newvalidationpasswordctl'].setErrors({ 'incorrect': true })
      return;
    }
    if (this.passwordform.valid) {
      this.userservice.changepwd(this.oldpassword, this.newpassword)
        .subscribe(() => {
          this.message = true;
        },
          error => {
            if (error.status==400) {
              this.passwordform.controls['oldpasswordctl'].setErrors({ 'incorrect': true })
            }
          });
    }
  }

}
