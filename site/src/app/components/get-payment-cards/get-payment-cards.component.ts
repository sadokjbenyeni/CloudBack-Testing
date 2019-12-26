import { Component, OnInit } from '@angular/core';
import { Payment } from '../../models/Payment';
import { CardType } from '../../models/CardType';

@Component({
  selector: 'app-get-payment-cards',
  templateUrl: './get-payment-cards.component.html',
  styleUrls: ['./get-payment-cards.component.css']
})
export class GetPaymentCardsComponent implements OnInit {

  constructor() { }
  cards: Payment[] = []
  ngOnInit() {
    let payment1 = new Payment();
    let payment2 = new Payment();
    payment1.expirationDate = new Date();
    payment1.cardType = CardType.MasterCard
    payment1.cardNumber = "1234123412341234"
    payment1.client = "yassin ben abderrabba"
    this.cards.push(payment1)
    let date2 = new Date()
    date2.setMonth(date2.getMonth() + 1)
    payment2.expirationDate = date2;
    payment2.cardType = CardType.Visa;
    payment2.cardNumber = "4321432143214321"
    payment2.client = "Sadok Jbenyani"
    this.cards.push(payment2)
  }

}
