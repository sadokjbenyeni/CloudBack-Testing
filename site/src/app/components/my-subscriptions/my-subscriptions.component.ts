import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { SubscriptionConfigurationPopupComponent } from '../subscription-configuration-popup/subscription-configuration-popup';
import { SubscriptionFilter } from '../../models/SubscriptionFilter';


@Component({
  selector: 'app-my-subscriptions',
  templateUrl: './my-subscriptions.component.html',
  styleUrls: ['./my-subscriptions.component.css']
})

export class MySubscriptionsComponent implements OnInit {
  DisplayedColumns: string[] = ['Subscriber', 'SubscriptionType','status'];
  dataSource;
  constructor(private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.fillDataSource()

  }
  fillDataSource() {
    this.subscriptionService.SubscriptionRequestsByUser().subscribe
      (result => {
        this.dataSource = result;
      })
  }
  configure(element: SubscriptionResult) {
    this.dialog.open(SubscriptionConfigurationPopupComponent, { width: '55vw', data: { subscription: element } })
  }
}
