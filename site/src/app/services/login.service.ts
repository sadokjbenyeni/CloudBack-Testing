import { GuardGuard } from './../guard.guard';
import { UserService } from './user.service';
import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthentificationService } from './authentification.service';
import { LoginComponent } from '../components/login/login.component';

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

  constructor(
    public router: Router,
    public userService: UserService,
    public activatedRoute: ActivatedRoute,
    public dialogRef: MatDialogRef<LoginComponent>,
    public authentificationService: AuthentificationService,
    public guardGuard: GuardGuard
  ) { }

  activate() {
    this.activatedRoute.params.subscribe(params => {
      this.userService.activation({ token: params.token }).subscribe(res => {
        this.message = res.message;
        this.colorMessage = 'alert alert-info';
        if (this.message === 'User Not Found') {
          this.colorMessage = 'alert alert-danger';
        } else {
          this.page = 'activation';
        }
      });
    });
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

  savemdp() {
    this.userService.mdpmodif({ token: this.token, pwd: this.password }).subscribe(res => {
      this.colorMessage = 'alert alert-info';
      this.message = 'Password successfully changed';
      setTimeout(() => {
        this.message = '';
        this.router.navigate(['/login']);
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
        this.router.navigateByUrl(this.guardGuard.router.routerState.snapshot.root.queryParams["redirection"]);
      }
    })
  };

  termsOpen() {
    this.viewterms = true;
  }
  termsClose() {
    this.viewterms = false;
  }
  closeDialog() {
    this.dialogRef.close();
  }
}
