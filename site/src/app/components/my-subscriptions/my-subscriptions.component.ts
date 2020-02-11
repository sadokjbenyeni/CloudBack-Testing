import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, Renderer2 } from '@angular/core';
import { SubscriptionService } from '../../services/subscription.service';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { Subscription } from '../../models/Subscription';
import { DatePipe } from '@angular/common';


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
  @ViewChild(MatPaginator, { static: false })
  paginator: MatPaginator;
  @ViewChild('container', { static: false }) private container: ElementRef;
  @ViewChild('details', { static: false }) details: ElementRef;
  @ViewChild('list', { static: false }) list: ElementRef;
  @ViewChild('paginate', { static: false }) paginate: ElementRef;



  constructor(private renderer: Renderer2, private datepipe: DatePipe, private subscriptionService: SubscriptionService) { }
  ngOnInit() {
    this.dataSource.filterPredicate = (data: Subscription, filter: string) =>
      data.type.toLocaleLowerCase().indexOf(filter) != -1 || data.status.toLocaleLowerCase().indexOf(filter) != -1 || this.datepipe.transform(data.creationDate, 'dd/MM/yyyy').indexOf(filter) != -1;

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
  selectsubscription(subscription: Subscription) {
    this.choosedsubscription = subscription;
    this.renderer.removeClass(this.container.nativeElement, "col-8")
    this.renderer.addClass(this.container.nativeElement, "col-12");
    this.renderer.removeClass(this.list.nativeElement, "col-12");
    this.renderer.addClass(this.list.nativeElement, "col-3");
    this.dataSource.paginator = null;
    this.paginate.nativeElement.remove();
  }
}
