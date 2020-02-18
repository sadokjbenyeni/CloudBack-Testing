import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Payment } from '../../../../models/Payment';
import { MatRadioChange } from '@angular/material';

@Component({
  selector: 'app-get-payment-cards',
  templateUrl: './get-payment-cards.component.html',
  styleUrls: ['./get-payment-cards.component.css']
})
export class GetPaymentCardsComponent implements OnInit {
  @Input() cards: Payment[] = [];
  @Input() radio: boolean = false;
  SelectedItem: Payment;
  @Output() choosingNewCard = new EventEmitter<Payment>();

  constructor() { }
  ngOnInit() {
  }
  PaymentChanged(event: MatRadioChange) {
    this.choosingNewCard.emit(event.value as Payment);
  }

}
