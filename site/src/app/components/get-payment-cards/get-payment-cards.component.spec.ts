import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GetPaymentCardsComponent } from './get-payment-cards.component';

describe('GetPaymentCardsComponent', () => {
  let component: GetPaymentCardsComponent;
  let fixture: ComponentFixture<GetPaymentCardsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GetPaymentCardsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GetPaymentCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
