import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MutualizedSubscriptionComponent } from './mutualized-subscription.component';

describe('MutualizedSubscriptionComponent', () => {
  let component: MutualizedSubscriptionComponent;
  let fixture: ComponentFixture<MutualizedSubscriptionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MutualizedSubscriptionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MutualizedSubscriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
