import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from '../../models/Subscription';
import { Observable } from 'rxjs';
import { PaymentService } from '../../services/payment.service';
import { Payment } from '../../models/Payment';

@Component({
  selector: 'app-subscriptions-details',
  templateUrl: './subscriptions-details.component.html',
  styleUrls: ['./subscriptions-details.component.css']
})
export class SubscriptionsDetailsComponent implements OnInit {

  constructor(private paymentService: PaymentService) { }
  @Input() events: Observable<void>;
  paymentMethod: Payment

  @Input('subscription') subscription: Subscription
  ngOnInit() {
    this.initializepayment();
    this.events.subscribe(() => {
      this.initializepayment()
    });
  }
  initializepayment() {
    this.paymentMethod == null;
    this.paymentService.GetPaymentCard(this.subscription.paymentMethodId).subscribe
      (result => {
        this.paymentMethod = result;

      })
  }

}
