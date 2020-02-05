import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { environment } from '../../environments/environment';
import { StatementBankingIdentities, StatementBankingIdentity } from '../models/StatementBankingIdentities';


@Injectable()
export class CurrencyService {

  constructor(private http: HttpClient) { }

  getCurrencies() {
    return this.http.get<StatementBankingIdentities>( environment.api + '/v1/currency');
  }

  getRib(rib) {
    return this.http.get<StatementBankingIdentity>( environment.api + '/v1/currency/rib/'+ rib);
  }

  saverib(rib) {
    return this.http.post( environment.api + '/v1/currency/saverib', rib);
  }

}
