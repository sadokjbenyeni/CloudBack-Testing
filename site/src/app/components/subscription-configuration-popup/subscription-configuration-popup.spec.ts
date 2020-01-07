import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscriptionConfigurationPopupComponent } from './subscription-configuration-popup';

describe('ConfigurationPopupComponent', () => {
  let component: SubscriptionConfigurationPopupComponent;
  let fixture: ComponentFixture<SubscriptionConfigurationPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubscriptionConfigurationPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscriptionConfigurationPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
