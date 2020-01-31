import { Injectable } from '@angular/core';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map'
import 'rxjs/Rx';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Roles, Pages } from '../models/Role';

@Injectable()
export class RoleService {

  constructor(private http: HttpClient) { }

  getRoles() {
    return this.http.get<Roles>( environment.api + '/role' ,);
  }

  getPages() {
    return this.http.get<Pages>( environment.api + '/role/page' );
  }

}
