import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { SubscriptionConfigurationPopupComponent } from '../subscription-configuration-popup/subscription-configuration-popup';
import { SubscriptionFilter } from '../../models/SubscriptionFilter';


@Component({
  selector: 'app-subscriptions-errors',
  templateUrl: './subscriptions-errors.component.html',
  styleUrls: ['./subscriptions-errors.component.css']
})

export class SubscriptionserrorsComponent implements OnInit {
  DisplayedColumns: string[] = ['Subscriber', 'OrderId', 'SubscriptionType', 'status', 'DeclineMessage'];
  dataSource;
  constructor(private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.fillDataSource()

  }
  fillDataSource() {
    this.subscriptionService.getSubscriptionsByFilter(SubscriptionFilter.Error).subscribe
      (result => {
        this.dataSource = result;
      })
  }
}
