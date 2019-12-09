import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TermsService } from '../../services/terms.service';
import { CountriesService } from '../../services/countries.service';
import { MatStepper } from '@angular/material';
import { Subscription } from '../../models/Subscription';
import { Country } from '../../models/Country';
import { SubscriptionType } from '../../models/SubsriptionType';
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
  IscgvAccepted: Boolean=false;

  @ViewChild('subscriptionstepper', { static: false }) stepper: MatStepper;
  Countries: Country[];
  constructor(private _formBuilder: FormBuilder, private termsService: TermsService, private countryService: CountriesService) { }

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
}