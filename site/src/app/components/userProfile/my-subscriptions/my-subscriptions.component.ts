import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, Renderer2 } from '@angular/core';
import { SubscriptionService } from '../../../services/subscription.service';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { Subscription } from '../../../models/Subscription';
import { DatePipe } from '@angular/common';
import { Subject } from 'rxjs/Subject';


@Component({
  selector: 'app-my-subscriptions',
  templateUrl: './my-subscriptions.component.html',
  styleUrls: ['./my-subscriptions.component.css']
})

export class MySubscriptionsComponent implements OnInit, AfterViewInit {
  DisplayedColumns: string[] = ['type', 'status', 'creationDate'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort, { static: false })
  sort: MatSort;
  choosedsubscription: Subscription;
  @ViewChild('container', { static: false }) private container: ElementRef;
  @ViewChild('details', { static: false }) details: ElementRef;
  @ViewChild('list', { static: false }) list: ElementRef;
  eventsSubject: Subject<void> = new Subject<void>();



  constructor(private renderer: Renderer2, private datepipe: DatePipe, private subscriptionService: SubscriptionService) { }
  ngOnInit() {
    this.dataSource.filterPredicate = (data: Subscription, filter: string) =>
      data.type.toLocaleLowerCase().indexOf(filter) != -1 || data.status.toLocaleLowerCase().indexOf(filter) != -1 || this.datepipe.transform(data.creationDate, 'dd/MM/yyyy').indexOf(filter) != -1;

    this.fillDataSource()
  }
  ngAfterViewInit() {
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
  selectsubscription(subscription: Subscription) {
    if (this.choosedsubscription != subscription) {
      this.choosedsubscription = subscription;
      this.eventsSubject.next();
    }
  }
}
