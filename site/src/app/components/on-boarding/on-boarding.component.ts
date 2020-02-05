import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-on-boarding',
  templateUrl: './on-boarding.component.html',
  styleUrls: ['./on-boarding.component.css']
})
export class OnBoardingComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
    let element = this.router.routerState.snapshot.root.queryParams['scrollTo'];
    if (element != undefined) {
      var el = document.getElementById(element);
      if (el != undefined) {
        el.scrollIntoView({ behavior: 'smooth' })
      }
    }
  }

}
