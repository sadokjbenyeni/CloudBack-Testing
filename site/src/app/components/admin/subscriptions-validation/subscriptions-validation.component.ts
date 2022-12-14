import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription.service';
import { MatDialog, MatSnackBar, MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { RejectionMessageDialogComponent } from '../../rejection-message-dialog/rejection-message-dialog.component';
import { Subscription } from '../../../models/Subscription';
import { ConfirmationPopupService } from '../../../services/confirmation-popup.service';
import { SubscriptionResult } from '../../../models/SubscriptionResult';
import { SubscriptionFilter } from '../../../models/SubscriptionFilter';


@Component({
  selector: 'app-subscriptions-validation',
  templateUrl: './subscriptions-validation.component.html',
  styleUrls: ['./subscriptions-validation.component.css']
})

export class SubscriptionsValidationComponent implements OnInit,AfterViewInit {
  DisplayedColumns: string[] = ['subscriber', 'orderId', 'type', 'Actions'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator; 
   constructor(private confirmationService: ConfirmationPopupService, private snackbar: MatSnackBar, private dialog: MatDialog, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.dataSource.filterPredicate = (data: Subscription, filter: string) =>
    data.subscriber.toLocaleLowerCase().indexOf(filter) != -1 || data.type.toLocaleLowerCase().indexOf(filter) != -1 || data.orderId.indexOf(filter) != -1
     this.fillDataSource()

  }
  ngAfterViewInit()
  {
    this.dataSource.paginator=this.paginator;
    this.dataSource.sort=this.sort;
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
        this.dataSource.data = result;
      })
  }
  applyFilter(value: any) {
    this.dataSource.filter = value.toLowerCase();
  }
}
