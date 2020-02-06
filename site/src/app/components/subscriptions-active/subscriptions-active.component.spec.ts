import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscriptionsactiveComponent } from './subscriptions-active.component';

describe('SubscriptionsactiveComponent', () => {
  let component: SubscriptionsactiveComponent;
  let fixture: ComponentFixture<SubscriptionsactiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubscriptionsactiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscriptionsactiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
