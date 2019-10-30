import { Subscription } from 'rxjs';
import { Injectable } from '@angular/core';
import { EventEmitter } from 'events';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  invokeLoginComponentFunction = new EventEmitter();
  sub: Subscription;

  constructor(
    private userService: UserService
  ) { }

  onLoginSucceeded(loggedUser: any) {
    this.invokeLoginComponentFunction.emit('onLoginSuccessed', loggedUser);
  }

  login(email: string, pwd: string) {
    sessionStorage.removeItem('user');
    localStorage.setItem('ula', 'false');
    var userResponse = this.userService.check({ email: email, pwd: pwd });
    userResponse.subscribe(res => {
      if(res.user)
      {
        let user =  JSON.stringify(res.user);
        sessionStorage.setItem('user',user);
        localStorage.setItem('ula', 'true');
        this.onLoginSucceeded(user);
      }
    });
    return userResponse;
  }
}
