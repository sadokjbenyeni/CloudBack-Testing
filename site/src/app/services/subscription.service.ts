import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Subscription } from '../models/Subscription';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private http: HttpClient) {
  }
  getPendingSubscriptions(): Observable<Subscription[]> {

    return this.http.get<any[]>(environment.api+"/v1/AdminSubscription/SubscriptionRequests/PendingValidation")
      .map(result => {
        var subscriptions: Subscription[] = [];
        result.forEach(element => {
          let subscription = new Subscription(element["type"])
          Object.assign(subscription, element);
          subscription.accountId = element["subscriptionAccountId"];
          subscriptions.push(subscription);
        })
        return subscriptions;
      });
  }
  getActiveSubscriptions(): Observable<Subscription[]> {

    return this.http.get<any[]>(environment.api+"/v1/AdminSubscription/SubscriptionRequests/PendingConfiguration")
      .map(result => {
        var subscriptions: Subscription[] = [];
        result.forEach(element => {
          let subscription = new Subscription(element["type"])
          Object.assign(subscription, element);
          subscription.accountId = element["subscriptionAccountId"];
          subscriptions.push(subscription);
        })
        return subscriptions;
      });
  }
  AcceptSubscription(id: string): Observable<any> {
    return this.http.put(environment.api+"/v1/AdminSubscription/validate", { subscriptionId: id })
  }
  RejectSubscription(id: string, message: string): Observable<any> {
    return this.http.put(environment.api+"/v1/AdminSubscription/decline", { subscriptionId: id, message: message });
  }
  ConfigureSubscription(id: string, message: string): Observable<any> {
    return this.http.put(environment.api+"/v1/AdminSubscription/configure", { subscriptionId: id, message: message });
  }
}

