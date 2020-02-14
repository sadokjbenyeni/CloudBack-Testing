import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription.service';
import {  MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
  import { SubscriptionFilter } from '../../../models/SubscriptionFilter';
import { Subscription } from '../../../models/Subscription';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-subscriptions-errors',
  templateUrl: './subscriptions-errors.component.html',
  styleUrls: ['./subscriptions-errors.component.css']
})

export class SubscriptionserrorsComponent implements OnInit,AfterViewInit {
  DisplayedColumns: string[] = ['subscriber', 'orderId', 'type', 'status', 'declineMessage','validatedOrDeclinedDate'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator; 

  constructor(private datepipe: DatePipe,private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.dataSource.filterPredicate = (data: Subscription, filter: string) =>
    data.subscriber.toLocaleLowerCase().indexOf(filter) != -1 || data.type.toLocaleLowerCase().indexOf(filter) != -1 || data.orderId.indexOf(filter) != -1 || data.status.toLocaleLowerCase().indexOf(filter) != -1
    || data.declineMessage.toLocaleLowerCase().indexOf(filter) != -1 || this.datepipe.transform(data.validatedOrDeclinedDate, 'dd/MM/yyyy').indexOf(filter) != -1;    
    this.fillDataSource()
  }
  ngAfterViewInit()
  {
    this.dataSource.paginator=this.paginator;
    this.dataSource.sort=this.sort;
  }
  fillDataSource() {
    this.subscriptionService.getSubscriptionsByFilter(SubscriptionFilter.Error).subscribe
      (result => {
        this.dataSource.data = result;
      })
  }
  applyFilter(value: any) {
    this.dataSource.filter = value.toLowerCase();
  }
}
