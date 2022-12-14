import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TermsService } from '../../services/terms.service';
import { CountriesService } from '../../services/countries.service';
import { MatStepper } from '@angular/material';
import { Subscription } from '../../models/Subscription';
import { Country } from '../../models/Country';
import { SubscriptionType } from '../../models/SubsriptionType';
import { User } from '../../models/User';
import { UserService } from '../../services/user.service';
import { Payment } from '../../models/Payment';
import { SubscriptionService } from '../../services/subscription.service';
@Component({
  selector: 'app-mutualized-subscription',
  templateUrl: './mutualized-subscription.component.html',
  styleUrls: ['./mutualized-subscription.component.css']
})
export class MutualizedSubscriptionComponent implements OnInit {
  CgvFormGroup: FormGroup;
  BillingFormGroup: FormGroup;
  PayementFormGroup: FormGroup;
  Subscription: Subscription;
  PaymentCard: Payment;
  IscgvAccepted: Boolean = false;
  user: User;
  @ViewChild('subscriptionstepper', { static: false }) stepper: MatStepper;
  Countries: Country[];
  constructor(private userService: UserService, private _formBuilder: FormBuilder, private termsService: TermsService, private countryService: CountriesService, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.Subscription = new Subscription(SubscriptionType.Mutualized);
    this.termsService.getLastSaleTerm().subscribe(result => {
      this.Subscription.cgv = result;

    })
    this.countryService.getCountries().subscribe(result => {
      this.Countries = result.countries;
    });
    this.CgvFormGroup = this._formBuilder.group({
      cgvCrtl: ['', Validators.required]
    });
    this.BillingFormGroup = this._formBuilder.group({
      VAT: ['', Validators.required],
      City: ['', Validators.required],
      Postal: ['', Validators.required],
      Address: ['', Validators.required],
      Country: ['', Validators.required],

    });
  }
  goto(step: number) {
    this.stepper.selectedIndex = step;
  }
  showOptions(event) {
    if (event.checked) {
      this.IscgvAccepted = true;
    }
    else {
      this.IscgvAccepted = false;
    }
  }


  GetPaymentMethod($event) {
    debugger;
    // this.Subscription.paymentCard = $event as Payment;
    this.PaymentCard = $event as Payment;
    this.Subscription.paymentMethodId = ($event as Payment).paymentMethodId;
  }
  AddPaymentMethod() {
    debugger;
    this.subscriptionService.CreateSubscriptionRequest(this.Subscription).subscribe
      (() => {
        console.log("Subscription request successfully Created")
      })
  }
  goNext(stepper: MatStepper) {
    if (this.PaymentCard == null)
      alert("please choose a valid card")
    else
      stepper.next();
  }
}