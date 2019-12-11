import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { RejectionMessageDialogComponent } from '../rejection-message-dialog/rejection-message-dialog.component';
import { Subscription } from '../../models/Subscription';


@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})

export class SubscriptionsComponent implements OnInit {
  DisplayedColumns: string[] = ['Id', 'Subscriber', 'SubscriptionType', 'Actions'];
  dataSource;
  constructor(private snackbar: MatSnackBar, private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.fillDataSource()

  }
  AcceptSubscription(subscription: Subscription) {
    this.subscriptionService.AcceptSubscription(subscription.id).subscribe
      (() => {
        this.snackbar.open("Subscription Accepted Sussesfully", "Ok", { duration: 3000 })
        this.fillDataSource()

      }
      )
  }
  RejectSubscription(subscription: Subscription) {
    this.dialog.open(RejectionMessageDialogComponent, { width: '55vw', data: { subscription: subscription } })
    .afterClosed().subscribe(
      result=>
      {
        if (result==true)
         {
          this.fillDataSource()
         }
      }
    )
  }
  fillDataSource() {
    this.subscriptionService.getAllSubscriptions().subscribe
      (result => {
        debugger;
        this.dataSource = result.filter(item => item["status"] == "Pending");

        debugger;
      })
  }
}
