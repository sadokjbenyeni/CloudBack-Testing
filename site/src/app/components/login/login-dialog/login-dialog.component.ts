import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../../services/login.service';
import { MatDialog } from '@angular/material';
import { PasswordComponent } from '../password/password.component';


@Component({
  selector: 'app-login',
  templateUrl: './login-Dialog.component.html',
  styleUrls: ['./login-Dialog.component.css']
})
export class LoginDialogComponent implements OnInit {

  constructor(
    private router: Router,
    public loginService: LoginService,
  ) {
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
      this.router.navigate(['/home']);
    }
    this.loginService.message = '';
    let register = sessionStorage.getItem('register');
    if (register === 'ok') {
      this.loginService.message = 'Your account has been created';
      this.loginService.colorMessage = 'alert alert-info'
    }
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
