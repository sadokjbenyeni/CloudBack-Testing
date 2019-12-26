import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Payment } from '../models/Payment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient) { }
  AddPaymentCard(card: Payment) {
    return this.http.post("dzaodjzaio", { card });
    return "ok";
  }

}
