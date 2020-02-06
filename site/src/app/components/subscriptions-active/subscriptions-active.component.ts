import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { SubscriptionConfigurationPopupComponent } from '../subscription-configuration-popup/subscription-configuration-popup';
import { SubscriptionFilter } from '../../models/SubscriptionFilter';


@Component({
  selector: 'app-subscriptions-active',
  templateUrl: './subscriptions-active.component.html',
  styleUrls: ['./subscriptions-active.component.css']
})

export class SubscriptionsactiveComponent implements OnInit {
  DisplayedColumns: string[] = ['Subscriber', 'OrderId', 'SubscriptionType','ActivationMessage','ActivationDate'];
  dataSource;
  constructor(private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.fillDataSource()

  }
  fillDataSource() {
    this.subscriptionService.getSubscriptionsByFilter(SubscriptionFilter.Active).subscribe
      (result => {
        this.dataSource = result;
      })
  }
  configure(element: SubscriptionResult) {
    this.dialog.open(SubscriptionConfigurationPopupComponent, { width: '55vw', data: { subscription: element } })
  }
}
