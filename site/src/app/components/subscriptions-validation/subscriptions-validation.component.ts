import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { RejectionMessageDialogComponent } from '../rejection-message-dialog/rejection-message-dialog.component';
import { Subscription } from '../../models/Subscription';
import { ConfirmationPopupService } from '../../services/confirmation-popup.service';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { SubscriptionFilter } from '../../models/SubscriptionFilter';


@Component({
  selector: 'app-subscriptions-validation',
  templateUrl: './subscriptions-validation.component.html',
  styleUrls: ['./subscriptions-validation.component.css']
})

export class SubscriptionsValidationComponent implements OnInit {
  DisplayedColumns: string[] = ['Subscriber', 'OrderId', 'SubscriptionType', 'Actions'];
  dataSource;
  constructor(private confirmationService: ConfirmationPopupService, private snackbar: MatSnackBar, private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.fillDataSource()

  }
  AcceptSubscription(subscription: SubscriptionResult) {
    this.confirmationService.openPopup('Are you sure to accept the ' + subscription.subscriptionType + ' subsciption number ' + subscription.orderId + ' of ' + subscription.subscriber + '?', "Accept Subscription")
      .subscribe((result: Boolean) => {
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
    this.subscriptionService.getSubscriptionsByFilter(SubscriptionFilter.PendingValidation).subscribe
      (result => {
        this.dataSource = result;
      })
  }
}
