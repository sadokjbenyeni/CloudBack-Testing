import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { RejectionMessageDialogComponent } from '../rejection-message-dialog/rejection-message-dialog.component';
import { Subscription } from '../../models/Subscription';
import { ConfirmationPopupService } from '../../services/confirmation-popup.service';


@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})

export class SubscriptionsComponent implements OnInit {
  DisplayedColumns: string[] = [ 'Subscriber', 'SubscriptionType', 'Actions'];
  dataSource;
  constructor(private confirmationService: ConfirmationPopupService, private snackbar: MatSnackBar, private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.fillDataSource()

  }
  AcceptSubscription(subscription: Subscription) {
    this.confirmationService.openPopup('Are you sure to accept the ' + subscription.subscriptionType + ' subsciption of ' + subscription.subscriber, "Accept Subscription")
      .subscribe((result: Boolean) => {
        debugger;
        if (result == true)
          this.subscriptionService.AcceptSubscription(subscription.id).subscribe
            (() => {
              this.snackbar.open("Subscription Accepted Sussesfully", "Ok", { duration: 3000 })
              this.fillDataSource()

            })
      })
  }
  RejectSubscription(subscription: Subscription) {
    this.dialog.open(RejectionMessageDialogComponent, { width: '55vw', data: { subscription: subscription } })
      .afterClosed().subscribe(
        result => {
          if (result == true) {
            this.fillDataSource()
          }
        }
      )
  }
  fillDataSource() {
    this.subscriptionService.getAllSubscriptions().subscribe
      (result => {
        this.dataSource = result.filter(item => item["status"] == "Pending");
      })
  }
}
