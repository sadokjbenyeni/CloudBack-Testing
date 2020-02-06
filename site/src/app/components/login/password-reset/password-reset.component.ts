import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../../services/login.service';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {

  constructor(
    public loginService: LoginService,
  ) {

   }

  ngOnInit() {
  }

  changePassword(){
    let currentToken = this.loginService.router.url.split('/');

    this.loginService.savemdp(currentToken[2], this.loginService.password);
  }
}
