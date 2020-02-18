import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {
  password: string;
  message: string;
  constructor(
    public userservice: UserService,
    public router: Router
  ) {

  }

  ngOnInit() {
  }

  changePassword() {

    this.userservice.resetpwd({ pwd: this.password }).subscribe(res => {
      this.message = 'Password successfully changed';
      setTimeout(() => {
        this.message = '';
        this.router.navigateByUrl('/login');
      }, 3000);
    });
  }
}
