import { Component, OnInit } from '@angular/core';
import { Subscription } from '../../models/Subscription';
import { SubscriptionService } from '../../services/subscription.service';


@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})

export class SubscriptionsComponent implements OnInit {
  DisplayedColumns: string[] = ['Id', 'AccountId', 'SubscriptionType', 'Actions'];
  dataSource;
  debugger;
  constructor(private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.subscriptionService.getAllSubscriptions().subscribe
      (result => {
        this.dataSource = result;
      })

  }

}
