import { PasswordComponent } from './../components/login/password/password.component';
import { GuardGuard } from './../guard.guard';
import { UserService } from './user.service';
import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {  MatDialog } from '@angular/material/dialog';
import { AuthentificationService } from './authentification.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  viewterms: boolean;
  ula: boolean;
  colorMessage: string;
  email: string;
  token: string;
  pwd: string;
  password: string;
  page: string;
  message: String;
  showAll = false;
  redirection: string;
  

  constructor(
    public router: Router,
    public userService: UserService,
    public activatedRoute: ActivatedRoute,
    public authentificationService: AuthentificationService,
    public guardGuard: GuardGuard,
    public passwordDialog: MatDialog,

  ) {
  }

  mdp() {
    this.userService.verifmail({ email: this.email }).subscribe(res => {
      if (!res.valid) {
        this.colorMessage = 'alert alert-danger';
        this.message = res.message;
      } else {
        this.userService.mdpmail({ email: this.email, token: res.token }).subscribe(r => {
          if (r.mail) {
            this.colorMessage = 'alert alert-info';
            this.message = 'An email has just been sent';
          } else {
            this.colorMessage = 'alert alert-danger';
            this.message = 'An error has occurred. Please try again';
          }
        });
      }
    });
  }

  savemdp(token: string, password: string) {
    this.userService.mdpmodif({ token: token, pwd: password }).subscribe(res => {
      this.colorMessage = 'alert alert-info';
      this.message = 'Password successfully changed';
      setTimeout(() => {
        this.message = '';
        this.router.navigateByUrl('/login');
      }, 3000);
    });
  }

  check() {
    this.authentificationService.login(this.email, this.password).subscribe(res => {
      let currentUrl = this.router.url.split('/');
      if (!res.user) {
        this.message = res.message;
        this.colorMessage = 'alert alert-danger';
      }
      else if (currentUrl[1].includes("redirection")) {
        console.info(this.redirection);
        this.redirection = this.guardGuard.router.routerState.snapshot.root.queryParams['redirection'];
        window.location.href = this.redirection;
      }
      else {
        console.info("redirect to home");
        console.debug("redirection value is null");
        this.router.navigateByUrl("/home");
      }
    })
  };

  openPasswordDialog(){
    this.passwordDialog.open(PasswordComponent,{panelClass: 'no-padding-dialog'});
  }
}
