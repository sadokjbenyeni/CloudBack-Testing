import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material';
import { GuardGuard } from '../../../guard.guard';
import { PasswordResetEmailComponent } from '../password-reset-email/password-reset-email.component';
import { UserService } from '../../../services/user.service';



@Component({
  selector: 'app-login',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css']
})
export class LoginDialogComponent implements OnInit {
  email: string;
  password: string;
  message: string;
  redirection: string;

  constructor(
    private router: Router,
    public userService: UserService,
    public passwordDialog: MatDialog,
    public guardGuard: GuardGuard,
    public diagref: MatDialogRef<LoginDialogComponent>

  ) {
  }

  ngOnInit() {

    let user = JSON.parse(localStorage.getItem('user'));
    if (user !== null && user !== {}) {
      this.router.navigate(['/home']);
    }
  }

  openSignupPage() {
    this.router.navigateByUrl('/signup')
  }
  openPasswordDialog() {
    this.passwordDialog.open(PasswordResetEmailComponent, { panelClass: 'no-padding-dialog' });
  }
  login() {
    localStorage.removeItem('user');
    localStorage.setItem('ula', 'false');
    this.userService.check({ email: this.email, pwd: this.password }).subscribe(res => {
      if (!res['token']) {
        this.message = res['message'];
      }
      else {
        localStorage.setItem('token', res['token']);
        localStorage.setItem('ula', 'true');
        //emit event so that the menu component refresh its displayed informations
        this.userService.invokeLoginComponentFunction.emit('onLoginSuccessed');

        let currentUrl = this.router.url.split('/');
        if (currentUrl[1].includes("redirection")) {
          this.redirection = this.guardGuard.router.routerState.snapshot.root.queryParams['redirection'];
          window.location.href = this.redirection;
        }
        else {
          this.router.navigateByUrl("/on-boarding");
        }
        this.diagref.close();
      }
    });
  }
}
