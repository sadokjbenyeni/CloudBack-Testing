import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Payment } from '../models/Payment';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient) { }
  AddPaymentCard(card: Payment) {
    return this.http.post(environment.api + "/v1/PaymentMethod", card);
  }
  getPaymentCards() {
    return this.http.get(environment.api + "/v1/PaymentMethod");
  }
  getAdminPaymentMethod(paymentMethodId: string): any {
    return this.http.get(environment.api + "/v1/AdminPayment/" + paymentMethodId)
  }
  GetPaymentCard(paymentMethodId: string): any {
    return this.http.get(environment.api + "/v1/PaymentMethod/" + paymentMethodId);
  }

}
