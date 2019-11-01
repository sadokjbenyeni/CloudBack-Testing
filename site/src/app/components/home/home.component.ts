import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  search: string;

  constructor(private data: DataService, private router: Router) {
  }

  ngOnInit() {
    this.data.currentSearch.subscribe(search => this.search = search);
    let element = this.router.routerState.snapshot.root.queryParams['scrollTo'];
    if (element != undefined) {
      var el = document.getElementById(element);
      if (el != undefined) {
        el.scrollIntoView({behavior:'smooth'})
      }
    }

  }

  changeSearch(dataset, search) {
    sessionStorage.setItem('dataset', JSON.stringify({ dataset: dataset, title: search }));
    this.data.changeSearch(search);
  }
}
