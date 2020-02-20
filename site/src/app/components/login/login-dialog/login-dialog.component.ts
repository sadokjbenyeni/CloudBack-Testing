import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material';
import { GuardGuard } from '../../../guard.guard';
import { AuthentificationService } from '../../../services/authentification.service';
import { PasswordResetEmailComponent } from '../password-reset-email/password-reset-email.component';



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
    public authenticationservice: AuthentificationService,
    public passwordDialog: MatDialog,
    public guardGuard: GuardGuard,
    public diagref: MatDialogRef<LoginDialogComponent>

  ) {
  }

  ngOnInit() {

    let user = JSON.parse(sessionStorage.getItem('user'));
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
    this.authenticationservice.login(this.email, this.password).subscribe(res => {
      let currentUrl = this.router.url.split('/');
      if (!res.user) {
        this.message = res.message;
      }
      else 
      {
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
