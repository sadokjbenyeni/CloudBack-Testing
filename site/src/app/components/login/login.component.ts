import { LoginService } from './../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(
    private router: Router,
    public loginService: LoginService
  ) {
  }

  ngOnInit() {
    // this.termspdf = '/files/historical_data_tc.pdf';
    this.loginService.viewterms = false;
    this.loginService.ula = false;
    let ula = localStorage.getItem('ula');
    if (ula !== null && ula !== '') {
      this.loginService.ula = (ula.toString() === 'true');
    }

    let user = JSON.parse(sessionStorage.getItem('user'));
    if (user !== null && user !== {}) {
      this.router.navigate(['/home']);
    }
    this.loginService.message = '';
    let register = sessionStorage.getItem('register');
    if (register === 'ok') {
      this.loginService.message = 'Your account has been created';
      this.loginService.colorMessage = 'alert alert-info'
    }
    // let url = this.router.url.split('/');
    // if (url[1] === "activation") {
    //   this.loginService.activate();
    // }
    this.loginService.email = '';
    this.loginService.token = '';
    this.loginService.pwd = '';
    this.loginService.password = '';
    let route = this.router.url.split('/');
    this.loginService.page = route[1];
    if (route[1] === 'mdp') {
      this.loginService.token = route[2];
    }
  }

  openSignupPage() {
    this.router.navigateByUrl('/signup')
  }
}
