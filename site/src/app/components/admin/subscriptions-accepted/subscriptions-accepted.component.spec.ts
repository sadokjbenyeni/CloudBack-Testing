import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscriptionsacceptedComponent } from './subscriptions-accepted.component';

describe('SubscriptionsacceptedComponent', () => {
  let component: SubscriptionsacceptedComponent;
  let fixture: ComponentFixture<SubscriptionsacceptedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubscriptionsacceptedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscriptionsacceptedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
