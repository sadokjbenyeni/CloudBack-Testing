import { LoginService } from './../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-login-page',
  template: ''
})
export class LoginPageComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private dialog: MatDialog,
    public loginService: LoginService,
  ) {
    this.openDialog(true);
  }

  ngOnInit() {
    this.loginService.viewterms = false;
    this.loginService.ula = false;
    let ula = localStorage.getItem('ula');
    if (ula !== null && ula !== '') {
      this.loginService.ula = (ula.toString() === 'true');
    }

    let user = JSON.parse(sessionStorage.getItem('user'));
    if (user !== null && user !== {}) {
      this.loginService.router.navigate(['/home']);
    }
    this.loginService.message = '';
    let register = sessionStorage.getItem('register');
    if (register === 'ok') {
      this.loginService.message = 'Your account has been created';
      this.loginService.colorMessage = 'alert alert-info'
    }
    // let url = this.loginService.router.url.split('/');
    // if (url[1] === "activation") {
    //   this.loginService.activate();
    // }
    this.loginService.email = '';
    this.loginService.token = '';
    this.loginService.pwd = '';
    this.loginService.password = '';
    let route = this.loginService.router.url.split('/');
    this.loginService.page = route[1];
    if (route[1] === 'mdp') {
      this.loginService.token = route[2];
    }
  }
  openDialog(canClose: boolean): void {
    this.dialog.open(LoginComponent, {panelClass: 'no-padding-dialog', disableClose: canClose, data: { source: this.route.parent.url }
    });
  }
}
