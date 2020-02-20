import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { CountriesService } from '../../../services/countries.service';
import { CompanytypesService } from '../../../services/companytypes.service';
import { User } from '../../../models/User';

export interface FormModel {
}

@Component({
  selector: 'app-general-informations',
  templateUrl: './general-informations.component.html',
  styleUrls: ['./general-informations.component.css']
})
export class GeneralInformationsComponent implements OnInit {
  user: User = new User();
  companyType: Array<object>;
  country: Array<object>;
  public formModel: FormModel = {};

  constructor(
    private userService: UserService,
    private countriesService: CountriesService,
    private companytypesService: CompanytypesService,
  ) { }
  ngOnInit() {

    this.getUser();
    this.getCompanyType();
    this.getCountry();

  }

  update() {
    this.userService.updateUser(this.user).subscribe(() => {
      let user = new User();
      debugger;
      user.token = this.user.token;
      user.roleName = this.user.roleName
      user.lastname=this.user.lastname;

      sessionStorage.setItem('user', JSON.stringify(user));
    });
  }
  getUser() {
    this.userService.getCompte().subscribe(res => {
      this.user = res;
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
}
