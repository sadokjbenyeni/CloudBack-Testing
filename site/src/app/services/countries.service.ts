import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map'
import 'rxjs/Rx';
import { environment } from '../../environments/environment';
import { Message } from '../models/message';
import { Countries, Country } from '../models/Country';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class CountriesService {

  constructor(private http: HttpClient) { }

  getCountries() :Observable<Countries>{
    return this.http.get<Countries>( environment.api + '/countries' );
  }
  isUE(id) {
    return this.http.post<Country>( environment.api + '/countries/isUE', id );
  }
  saveUE(country) {
    return this.http.put<Message>( environment.api + '/countries/ue', country );
  }

}
