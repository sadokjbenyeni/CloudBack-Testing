import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Payment } from '../../../../models/Payment';
import { PaymentService } from '../../../../services/payment.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  cards: Payment[] = [];
  @Input() showradio = false;
  @Output() emitTomother = new EventEmitter<Payment>();
  constructor(private paymentService: PaymentService) {

  }
  receivemessage($event) {
    var payment = ($event as Payment)
    this.cards.push(this.hidecardNumber(payment));
  }
  ngOnInit() {
    this.paymentService.getPaymentCards().subscribe((result: Payment[]) => {
      result.forEach(element=>
        {
          element.numbers= "***-"+element.numbers;
        })
      this.cards = result;
    })
  }
  hidecardNumber(payment: Payment): Payment {
    payment.numbers = "***-" + payment.numbers.substring(payment.numbers.length - 4, payment.numbers.length)
    return payment;
  }
  paymentchoosed($event) {
    this.emitTomother.emit($event as Payment);
  }
}
