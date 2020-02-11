import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatDialog, MatSnackBar, MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { SubscriptionConfigurationPopupComponent } from '../subscription-configuration-popup/subscription-configuration-popup';
import { SubscriptionFilter } from '../../models/SubscriptionFilter';
import { Subscription } from '../../models/Subscription';


@Component({
  selector: 'app-my-subscriptions',
  templateUrl: './my-subscriptions.component.html',
  styleUrls: ['./my-subscriptions.component.css']
})

export class MySubscriptionsComponent implements OnInit, AfterViewInit {
  DisplayedColumns: string[] = ['subscriber', 'type', 'status'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private subscriptionService: SubscriptionService) { }
  ngOnInit() {
    this.dataSource.filterPredicate = (data: Subscription, filter: string) =>
      data.subscriber.toLocaleLowerCase().indexOf(filter) != -1 || data.type.toLocaleLowerCase().indexOf(filter) != -1 || data.status.toLocaleLowerCase().indexOf(filter) != -1

    this.fillDataSource()
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  fillDataSource() {
    this.subscriptionService.SubscriptionRequestsByUser().subscribe
      (result => {
        this.dataSource.data = result;
      })
  }
  applyFilter(value: any) {
    this.dataSource.filter = value.toLowerCase();
  }
}
