import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { SubscriptionConfigurationPopupComponent } from '../subscription-configuration-popup/subscription-configuration-popup';
import { SubscriptionFilter } from '../../models/SubscriptionFilter';


@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})

export class SubscriptionsComponent implements OnInit {
  DisplayedColumns: string[] = ['Subscriber', 'OrderId', 'SubscriptionType', 'Status'];
  dataSource;
  constructor(private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.fillDataSource()

  }
  fillDataSource() {
    this.subscriptionService.getSubscriptionsByFilter(SubscriptionFilter.All).subscribe
      (result => {
        this.dataSource = result;
      })
  }
  configure(element: SubscriptionResult) {
    this.dialog.open(SubscriptionConfigurationPopupComponent, { width: '55vw', data: { subscription: element } })
  }
}
