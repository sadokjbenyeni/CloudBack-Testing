import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from '../../models/Subscription';

@Component({
  selector: 'app-subscriptions-details',
  templateUrl: './subscriptions-details.component.html',
  styleUrls: ['./subscriptions-details.component.css']
})
export class SubscriptionsDetailsComponent implements OnInit {

  constructor() { }
  @Input('subscription') subscription: Subscription
  ngOnInit() {
  }

}
