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


@Injectable()
export class UserService {

  protected authenticatedUser: boolean;

  constructor(private http: HttpClient) {
    this.authenticatedUser = false;
    this.getAuthenticatedUser();
  }

  create(user) {
    return this.http.post(environment.api + '/v1/user', user);
  }

  activation(token) {
    return this.http.post<LoginResponse>(environment.api + '/v1/user/activation', { token: token });
  }

  check(user) {
    return this.http.post<UserResponse>(environment.api + '/v1/user/check', user);
  }

  info(user) {
    return this.http.post(environment.api + '/v1/user/info', user);
  }

  getUsers() {
    return this.http.get<Users>(environment.api + '/v1/user');
  }

  getRoles() {
    return this.http.get<Roles>(environment.api + '/v1/role');
  }

  getCompte() {
    return this.http.get<User>(environment.api + '/v1/user/informations');
  }

  updateUser(user) {
    return this.http.put<Message>(environment.api + '/v1/user', user);
  }

  mdpmail(val) {
    return this.http.post<MailResponse>(environment.api + '/v1/mail/mdp', val);
  }
  changepwd(oldpwd, newpwd) {
    return this.http.put(environment.api + '/v1/user/changepwd', { old: oldpwd, new: newpwd });
  }
  
  resetpwd(val) {
    return this.http.put(environment.api + '/v1/user/resetpwd', val);
  }

  public getAuthenticatedUser() {
    return this.authenticatedUser;
  }

  logout(token) {
    return this.http.post(environment.api + '/v1/user/logout', token);
  }
  preferBilling(prefer) {
    return this.http.post(environment.api + '/v1/user/preferBilling', prefer);
  }

  verifmail(email) {
    return this.http.post<LoginResponse>(environment.api + '/v1/user/verifmail', email);
  }

}
