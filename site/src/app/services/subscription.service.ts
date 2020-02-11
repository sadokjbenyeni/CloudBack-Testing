import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Subscription } from '../models/Subscription';
import { environment } from '../../environments/environment';
import { SubscriptionType } from '../models/SubsriptionType';
import { SubscriptionFilter } from '../models/SubscriptionFilter';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private http: HttpClient) { }

  getSubscriptionsByFilter(SubscriptionFilter: SubscriptionFilter): Observable<any[]> {
    return this.http.get<Subscription[]>(environment.api + "/v1/AdminSubscription/SubscriptionRequests/" + SubscriptionFilter)
      .map(result => {
        return this.MapSubscription(result)
      })
  }
  SubscriptionRequestsByUser(): Observable<any> {
    return this.http.get<any[]>(environment.api + "/v1/SubscriptionRequest").map
      (result => {
        return this.MapSubscription(result)
      })
  }

  AcceptSubscription(id: string): Observable<any> {
    return this.http.put(environment.api + "/v1/AdminSubscription/validate", { subscriptionId: id })
  }

  RejectSubscription(id: string, message: string): Observable<any> {
    return this.http.put(environment.api + "/v1/AdminSubscription/decline", { subscriptionId: id, message: message });
  }

  ConfigureSubscription(id: string, message: string): Observable<any> {
    return this.http.put(environment.api + "/v1/AdminSubscription/configure", { subscriptionId: id, message: message });
  }

  CreateSubscriptionRequest(subscription: Subscription): Observable<any> {
    return this.http.post(environment.api + "/v1/SubscriptionRequest", subscription)
  }

  private MapSubscription(result: Array<any>): Array<any> {
    var subscriptions: Subscription[] = [];
    result.forEach(element => {
      let subscription = new Subscription(element["type"])
      Object.assign(subscription, element);
      subscription.orderId = element["orderId"].toString();
      subscription.accountId = element["subscriptionAccountId"];
      subscriptions.push(subscription);
    })
    return subscriptions;
  }
}

