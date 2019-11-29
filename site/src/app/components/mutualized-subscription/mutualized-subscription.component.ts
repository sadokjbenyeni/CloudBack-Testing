import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TermsService } from '../../services/terms.service';
import { Term } from '../../models/Terms';
import { CountriesService } from '../../services/countries.service';
import { Countries } from '../../models/Country';
@Component({
  selector: 'app-mutualized-subscription',
  templateUrl: './mutualized-subscription.component.html',
  styleUrls: ['./mutualized-subscription.component.css']
})
export class MutualizedSubscriptionComponent implements OnInit {
  CgvFormGroup: FormGroup;
  PaymentFormGroup: FormGroup;
  Cgv: Term;
  Countries: Array<Object>;
  constructor(private _formBuilder: FormBuilder, private terms: TermsService, private countryService: CountriesService) { }

  ngOnInit() {
    this.terms.getLastSaleTerm().subscribe(result => {
      this.Cgv = result;

    })
    this.countryService.getCountries().subscribe(result => {
      this.Countries = result.countries;
    });
    this.CgvFormGroup = this._formBuilder.group({
      cgvCrtl: ['', Validators.required]
    });
    this.PaymentFormGroup = this._formBuilder.group({
      VAT: ['', Validators.required],
      City: ['', Validators.required],
      Postal: ['', Validators.required],
      Adress: ['', Validators.required],
      Country: ['', Validators.required],
      
    });
  }
}