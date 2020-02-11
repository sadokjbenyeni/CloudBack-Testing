import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar, MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { SubscriptionConfigurationPopupComponent } from '../subscription-configuration-popup/subscription-configuration-popup';
import { SubscriptionFilter } from '../../models/SubscriptionFilter';
import { Subscription } from '../../models/Subscription';


@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})

export class SubscriptionsComponent implements OnInit, AfterViewInit {

  DisplayedColumns: string[] = ['subscriber', 'orderId', 'type', 'status'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  constructor(private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.dataSource.filterPredicate = (data: Subscription, filter: string) =>
      data.status.toLocaleLowerCase().indexOf(filter) != -1 || data.subscriber.toLocaleLowerCase().indexOf(filter) != -1 || data.type.toLocaleLowerCase().indexOf(filter) != -1 || data.orderId.indexOf(filter) != -1;
    this.fillDataSource()
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  fillDataSource() {
    this.subscriptionService.getSubscriptionsByFilter(SubscriptionFilter.All).subscribe
      (result => {
        this.dataSource.data = result;
      })
  }
  applyFilter(value: any) {
    this.dataSource.filter = value.toLowerCase();
  }
}
