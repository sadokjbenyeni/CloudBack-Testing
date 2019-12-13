import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscriptionsValidationComponent } from './subscriptions-validation.component';

describe('SubscriptionsComponent', () => {
  let component: SubscriptionsValidationComponent;
  let fixture: ComponentFixture<SubscriptionsValidationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubscriptionsValidationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscriptionsValidationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
