import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Subscription } from '../models/Subscription';
import { map } from 'rxjs-compat/operator/map';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private http: HttpClient) {
  }
  getAllSubscriptions(): Observable<Subscription[]> {

    return this.http.get<any[]>("https://localhost:5001/api/subscriptionRequest")
      .map(result => {
        var model = new Subscription(result["type"]);
        Object.assign(model, result);
        model.AccountId = result["subscriptionAccountId"];
        return result;
      })

  }
}

