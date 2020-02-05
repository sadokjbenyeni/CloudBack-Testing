import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { SubscriptionConfigurationPopupComponent } from '../subscription-configuration-popup/subscription-configuration-popup';


@Component({
  selector: 'app-subscriptions-accepted',
  templateUrl: './subscriptions-accepted.component.html',
  styleUrls: ['./subscriptions-accepted.component.css']
})

export class SubscriptionsacceptedComponent implements OnInit {
  DisplayedColumns: string[] = ['Subscriber', 'OrderId', 'SubscriptionType', 'Actions'];
  dataSource;
  constructor(private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.fillDataSource()

  }
  fillDataSource() {
    this.subscriptionService.getActiveSubscriptions().subscribe
      (result => {
        this.dataSource = result;
      })
  }
  configure(element: SubscriptionResult) {
    this.dialog.open(SubscriptionConfigurationPopupComponent, { width: '55vw', data: { subscription: element } })
  }
}
