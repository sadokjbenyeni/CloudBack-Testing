import { Injectable } from '@angular/core';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Users, User, UserResponse } from '../models/User';
import { Roles } from '../models/Role';
import { LoginResponse } from '../models/LoginResponse';
import { MailResponse } from '../models/MailResponse';
import { Message } from '../models/message';
import { UserDetailComponent } from '../components/admin/user-detail/user-detail.component';

// export interface User {
//   islogin: boolean;
// }

// const ANONYMOUS_USER = <User>{
//   islogin: false
// };

@Injectable()
export class UserService {

  protected authenticatedUser: boolean;

  constructor(private http: HttpClient) {
    this.authenticatedUser = false;
    this.getAuthenticatedUser();
  }

  create(user) {
    return this.http.post(environment.api + '/user', user);
  }

  activation(token) {
    return this.http.post<LoginResponse>(environment.api + '/user/activation', { token: token });
  }

  check(user) {

    return this.http.post<UserResponse>(environment.api + '/user/check', user);
  }

  info(user) {
    return this.http.post(environment.api + '/user/info', user);
  }

  getUsers() {
    return this.http.get<Users>(environment.api + '/user');
  }

  getRoles() {
    return this.http.get<Roles>(environment.api + '/role');
  }

  getCompte(user) {
    // var token = JSON.parse(sessionStorage.getItem("user"))["token"]
    // ,{headers:{authorization: token}
    return this.http.get<User>(environment.api + '/user/info');
  }

  updateUser(user) {
    return this.http.put<Message>(environment.api + '/user', user);
  }

  islogin(token) {
    return this.http.post<LoginResponse>(environment.api + '/user/islogin', token);
  }

  mdpmail(val) {
    return this.http.post<MailResponse>(environment.api + '/mail/mdp', val);
  }


  mdpmodif(val) {
    return this.http.put(environment.api + '/user/mdpmodif', val);
  }

  public getAuthenticatedUser() {
    return this.authenticatedUser;
  }

  logout(token) {
    return this.http.post(environment.api + '/user/logout', token);
  }
  preferBilling(prefer) {
    return this.http.post(environment.api + '/user/preferBilling', prefer);
  }

  verifmail(email) {
    return this.http.post<LoginResponse>(environment.api + '/user/verifmail', email);
  }

}
