import { Component, OnInit } from "@angular/core";
import { UserService } from "../../../services/user.service";
import { CountriesService } from "../../../services/countries.service";
import { CompanytypesService } from "../../../services/companytypes.service";
import { User } from "../../../models/User";
import { MatSnackBar } from "@angular/material";
import {
  FormGroup,
  FormBuilder,
  Validators,
} from "@angular/forms";
import * as jwt_decoder from "jwt-decode";

export interface FormModel {}

@Component({
  selector: "app-general-informations",
  templateUrl: "./general-informations.component.html",
  styleUrls: ["./general-informations.component.css"]
})
export class GeneralInformationsComponent implements OnInit {
  GeneralInformatioFrmGroup: FormGroup;
  user: User = new User();
  companyType: Array<object>;
  exist: boolean;
  country: Array<object>;
  email: string;
  public formModel: FormModel = {};

  constructor(
    private formbuilder: FormBuilder,
    private userService: UserService,
    private countriesService: CountriesService,
    private companytypesService: CompanytypesService,
    private snackbar: MatSnackBar
  ) {}
  ngOnInit() {
    this.GeneralInformatioFrmGroup = this.formbuilder.group({
      firstnamectl: ["", Validators.required],
      lastnamectl: ["", Validators.required],
      jobctl: ["", Validators.required],
      phonectl: ["", Validators.pattern("[0-9]{9,16}")],
      companynamectl: ["", Validators.required],
      companytypectl: [""],
      countryctl: ["", Validators.required],
      addressctl: [""],
      postalcodectl: [""],
      cityctl: [""],
      regionctl: [""],
      websitectl: [""]
    });
    this.user = new User();


    this.getUser();
    this.getCompanyType();
    this.getCountry();
  }

  update() {
    if(!this.GeneralInformatioFrmGroup.valid){
      return;
    }
    this.bindUser();
    localStorage.setItem("user", JSON.stringify(this.user));
    this.userService.updateUser(this.user).subscribe(() => {

    this.snackbar.open("Profile Successfully updated", "", {
        duration: 3000
      });
    });
  }
  getUser() {
    this.userService.getCompte().subscribe(res => {
      this.GeneralInformatioFrmGroup.controls["firstnamectl"].setValue(
        res.firstname
      );
      this.GeneralInformatioFrmGroup.controls["lastnamectl"].setValue(
        res.lastname
      );
      this.email = res.email;
      this.GeneralInformatioFrmGroup.controls["jobctl"].setValue(res.job);
      this.GeneralInformatioFrmGroup.controls["companynamectl"].setValue(
        res.companyName
      );
      this.GeneralInformatioFrmGroup.controls["companytypectl"].setValue(
        res.companyType
      );
      this.GeneralInformatioFrmGroup.controls["phonectl"].setValue(res.phone);
      this.GeneralInformatioFrmGroup.controls["countryctl"].setValue(
        res.country
      );
      this.GeneralInformatioFrmGroup.controls["addressctl"].setValue(
        res.address
      );
      this.GeneralInformatioFrmGroup.controls["postalcodectl"].setValue(
        res.postalCode
      );
      this.GeneralInformatioFrmGroup.controls["cityctl"].setValue(res.city);
      this.GeneralInformatioFrmGroup.controls["regionctl"].setValue(res.region);
      this.GeneralInformatioFrmGroup.controls["websitectl"].setValue(
        res.website
      );
    });
  }

  getCompanyType() {
    this.companytypesService.getCompanytypes().subscribe(res => {
      this.companyType = res.companytypes;
    });
  }
  getCountry() {
    this.countriesService.getCountries().subscribe(res => {
      this.country = res.countries;
    });
  }
  bindUser() {
    this.user.lastname = this.GeneralInformatioFrmGroup.controls[
      "lastnamectl"
    ].value;
    this.user.firstname = this.GeneralInformatioFrmGroup.controls[
      "firstnamectl"
    ].value;
    this.user.job = this.GeneralInformatioFrmGroup.controls["jobctl"].value;
    this.user.companyName = this.GeneralInformatioFrmGroup.controls[
      "companynamectl"
    ].value;
    this.user.companyType = this.GeneralInformatioFrmGroup.controls[
      "companytypectl"
    ].value;
    this.user.phone = this.GeneralInformatioFrmGroup.controls[
      "phonectl"
    ].value;
    this.user.country = this.GeneralInformatioFrmGroup.controls[
      "countryctl"
    ].value;
    this.user.address = this.GeneralInformatioFrmGroup.controls[
      "addressctl"
    ].value;
    this.user.postalCode = this.GeneralInformatioFrmGroup.controls[
      "postalcodectl"
    ].value;
    this.user.city = this.GeneralInformatioFrmGroup.controls["cityctl"].value;
    this.user.region = this.GeneralInformatioFrmGroup.controls[
      "regionctl"
    ].value;
    this.user.website = this.GeneralInformatioFrmGroup.controls[
      "websitectl"
    ].value;
  }

}
