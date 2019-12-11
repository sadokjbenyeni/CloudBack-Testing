import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Subscription } from '../models/Subscription';
import { map } from 'rxjs-compat/operator/map';
import { SubscriptionsComponent } from '../components/subscriptions/subscriptions.component';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private http: HttpClient) {
  }
  getAllSubscriptions(): Observable<Subscription[]> {

    return this.http.get<any[]>("https://localhost:5001/api/subscriptionRequest")
      .map(result => {
        var subscriptions: Subscription[] = [];
        result.forEach(element => {
          let subscription = new Subscription(element["type"])
          Object.assign(subscription, element);
          subscription.AccountId = element["subscriptionAccountId"];
          subscriptions.push(subscription);
        })
        return subscriptions;
      });
  }
  AcceptSubscription(id: string): Observable<any> {
    return this.http.put("https://localhost:5001/api/AdminSubscription/validate", { id: id })
  }
  RejectSubscription(id: string, message: string): Observable<any> {
    return this.http.put("https://localhost:5001/api/AdminSubscription/decline", { id: id, message: message });
  }
}

