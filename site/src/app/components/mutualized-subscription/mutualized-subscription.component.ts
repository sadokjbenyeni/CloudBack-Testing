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
import { TimeInterval } from 'rxjs/Rx';
import { timer } from 'rxjs';
import { Payment } from '../../models/Payment';
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
  IscgvAccepted: Boolean = false;
  user: User;
  @ViewChild('subscriptionstepper', { static: false }) stepper: MatStepper;
  Countries: Country[];
  constructor(private userService: UserService, private _formBuilder: FormBuilder, private termsService: TermsService, private countryService: CountriesService) { }

  ngOnInit() {
    this.Subscription = new Subscription(SubscriptionType.Mutualized);
    debugger;
    this.termsService.getLastSaleTerm().subscribe(result => {
      this.Subscription.cgv = result;

    })
    this.countryService.getCountries().subscribe(result => {
      this.Countries = result.countries;

      //AddBilling
      this.SetDefaultBillingInformations();

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
  SetDefaultBillingInformations() {
    let id = JSON.parse(sessionStorage.getItem('user'))._id;
    this.userService.getCompte(id).subscribe(res => {
      this.Subscription.billing.address = res.addressBilling;
      this.Subscription.billing.city = res.cityBilling;
      this.Subscription.billing.vat = res.vat;
      this.Subscription.billing.postalCode = res.postalCodeBilling;
      this.Subscription.billing.country = this.Countries.find(item => item.id == res.countryBilling)
    });
  }

  GetPaymentMethod($event) {
    this.Subscription.paymentCard = $event as Payment;
    debugger;

  }
  goNext(stepper: MatStepper) {
    if (this.Subscription.paymentCard == null)
      alert("please choose a valid card")
    else
      stepper.next();
  }
}