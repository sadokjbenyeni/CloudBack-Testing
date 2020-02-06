import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent implements OnInit {

  pageConfig: { page: string; active: boolean; link: string; }[];
  constructor() { }

  ngOnInit() {
    this.pageConfig = [
      { page: 'All Subscriptions', active: false, link: '/subscriptions' },
      { page: 'Subscriptions validation', active: false, link: '/subscriptions-validation' },
      { page: 'Subscriptions configuration ', active: false, link: '/subscriptions-accepted' },
      { page: 'Active Subscriptions ', active: false, link: '/subscriptions-active' },
      { page: 'Subscriptions Errors ', active: false, link: '/subscriptions-errors' },

      { page: 'Roles', active: false, link: '/admin/role' },
      { page: 'Users', active: false, link: '/admin/users' },
      { page: 'Countries', active: false, link: '/admin/countries' },
      { page: 'Terms', active: false, link: '/admin/terms' },
    ];
  }
}
