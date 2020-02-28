import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Payment } from '../../../../models/Payment';
import { CardType } from '../../../../models/CardType';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PaymentService } from '../../../../services/payment.service';
import { ConfirmationPopupService } from '../../../../services/confirmation-popup.service';
import { MatSnackBar } from '@angular/material';


@Component({
  selector: 'app-add-payment-card',
  templateUrl: './add-payment-card.component.html',
  styleUrls: ['./add-payment-card.component.css']
})
export class AddPaymentCardComponent implements OnInit {

  constructor(private matsnackbar: MatSnackBar, private formBuilder: FormBuilder, private paymentService: PaymentService, private confirmationpopupservice: ConfirmationPopupService) { }
  cardNumber: string;
  @Output() eventMessage = new EventEmitter<Payment>()
  Payment: Payment = new Payment();
  cardTypes: CardType[] = [];
  controlPaymentgrp: FormGroup;
  years: number[] = [];
  mounths: number[] = [1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12];
  currentDate: Date = new Date();
  expirationyear: string;
  expirationmonth: string;

  ngOnInit() {
    this.currentDate.setDate(1)
    let thisyear = this.currentDate.getFullYear();
    let nextyears = thisyear + 20;
    while (thisyear <= nextyears) {
      this.years.push(thisyear++)
    }
    this.controlPaymentgrp = this.formBuilder.group({
      numberctl: ['', [Validators.pattern("([0-9 \s]{13,23})"), Validators.required]],
      cardholerctl: ['', Validators.required],
      visialcryptoctl: ['', [Validators.required, Validators.pattern("[0-9]{3}")]],
      expmonthctl: ['', Validators.required],
      exptearctl: ['', Validators.required],
    });
  }
  onSubmitForm() {
    if (this.controlPaymentgrp.valid) {
      let valid = true
      if (this.IsExpirationDateValid()) {
        this.controlPaymentgrp.get('expmonthctl').setErrors({ 'incorrect': true })
        this.controlPaymentgrp.get('exptearctl').setErrors({ 'incorrect': true })
        valid = false;
      }
      if (!this.isCardValid(this.cardNumber.toString())) {
        this.controlPaymentgrp.get('numberctl').setErrors({ 'incorrect': true })
        valid = false;
      }
      if (valid == false) {
        return;
      }
      this.Payment.network = this.getCardType();
      this.Payment.numbers = this.cardNumber.replace(/ /gi, '');
      this.confirmationpopupservice.openPopup("Are you sure you want to add this card?", "Add PaymentCard")
        .subscribe(result => {
          if (result == true)
            this.paymentService.AddPaymentCard(this.Payment).subscribe(retult => {
              this.Payment.paymentMethodId = result["id"]
              this.eventMessage.emit(this.Payment);
              this.matsnackbar.open("Card Successfully Added", "Ok", { duration: 2000 });
              this.Payment = new Payment();
              
              this.controlPaymentgrp.reset();
            }, error => {
              debugger;
              this.matsnackbar.open(error.error, "", { duration: 3000 });
            });
        });
    }
  }
  addWhiteSpaces() {
    let realnumber = this.cardNumber.replace(/ /gi, '');
    for (let i = realnumber.length - 1; i >= 4; i--) {
      if (i % 4 == 0) {
        realnumber = realnumber.slice(0, i) + ' ' + realnumber.slice(i);
      }
    }
    this.cardNumber = realnumber;
  }
  getCardType(): CardType {
    if (this.cardNumber != undefined) {
      switch (this.cardNumber[0]) {
        case '3': return CardType.AmericanExpress
        case '4': return CardType.Visa
        case '5': return CardType.MasterCard
        case '6': return CardType.DiscoverCard
        default: return CardType.Other
      }

    }
  }
  IsExpirationDateValid(): boolean {
    let date = new Date();
    date.setFullYear(Number.parseInt(this.expirationyear), Number.parseInt(this.expirationmonth), null);
    this.Payment.expirationMonth = this.expirationmonth.toString();
    this.Payment.expirationYear = this.expirationyear.toString();
    return (date < this.currentDate);
  }

  isCardValid(value: string): boolean {
    value = value.replace(/ /gi, '');
    let sum = 0;
    for (let i = 0; i < 16; i++) {
      let currentintval = Number.parseInt(value[i]);
      if (i % 2 == 0) {
        sum += this.getsumofnumber(currentintval * 2)
      }
      else
        sum += currentintval;
    }
    return sum % 10 == 0;
  }
  getsumofnumber(value: number): number {
    if (Math.floor(value / 10) == 0)
      return value;
    else {
      return Math.floor(value / 10) + this.getsumofnumber(value % 10)
    }
  }
}
