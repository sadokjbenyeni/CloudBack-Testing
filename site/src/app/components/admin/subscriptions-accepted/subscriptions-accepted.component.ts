import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription.service';
import { MatDialog, MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { SubscriptionResult } from '../../../models/SubscriptionResult';
import { SubscriptionConfigurationPopupComponent } from '../../subscription-configuration-popup/subscription-configuration-popup';
import { SubscriptionFilter } from '../../../models/SubscriptionFilter';
import 'rxjs/Rx';
import { Subscription } from '../../../models/Subscription';

@Component({
  selector: 'app-subscriptions-accepted',
  templateUrl: './subscriptions-accepted.component.html',
  styleUrls: ['./subscriptions-accepted.component.css']
})

export class SubscriptionsacceptedComponent implements AfterViewInit, OnInit {



  DisplayedColumns: string[] = ['subscriber','orderId','type','Actions']

  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private dialog: MatDialog, private subscriptionService: SubscriptionService) {
  }
  ngOnInit(): void {
    this.dataSource.filterPredicate = (data: Subscription, filter: string) => data.subscriber.toLocaleLowerCase().indexOf(filter) != -1 || data.type.toLocaleLowerCase().indexOf(filter) != -1 || data.orderId.indexOf(filter) != -1;
    this.fillDataSource()
  }
  ngAfterViewInit(): void {
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
  }



  fillDataSource() {
    this.subscriptionService.getSubscriptionsByFilter(SubscriptionFilter.PendingConfiguration).subscribe
      (result => {
        this.dataSource.data = result;

      })
  }
  configure(element: SubscriptionResult) {
    this.dialog.open(SubscriptionConfigurationPopupComponent, { width: '55vw', data: { subscription: element } })
  }
  applyFilter(value: any) {
    this.dataSource.filter = value.toLowerCase();
  }
}
