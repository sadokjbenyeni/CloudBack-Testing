import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Payment } from '../models/Payment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient) { }
  AddPaymentCard(card: Payment) {
    return this.http.post("https://localhost:4001/api/PaymentMethod", { card });
  }
  getPaymentCards() {
    return this.http.get("https://localhost:4001/api/PaymentMethod");
  }

}
