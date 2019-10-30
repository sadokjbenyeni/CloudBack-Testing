import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Term } from '../models/Terms';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TermsService {

  constructor(private http: HttpClient) { }

  getLastTerm()
  {
    return this.http.get<Term>(environment.api+"/terms/lastterm");
  }
  
}
