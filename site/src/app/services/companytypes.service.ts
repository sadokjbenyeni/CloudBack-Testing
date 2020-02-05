import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/catch';
import { environment } from '../../environments/environment';
import { Companytypes } from '../models/Companytypes';

@Injectable()
export class CompanytypesService {

  constructor(private http: HttpClient) { }
  getCompanytypes() {
    return this.http.get<Companytypes>( environment.api + '/v1/companytype' );
  }
}
