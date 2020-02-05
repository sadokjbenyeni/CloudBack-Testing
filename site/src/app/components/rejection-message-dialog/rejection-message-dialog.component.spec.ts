import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RejectionMessageDialogComponent } from './rejection-message-dialog.component';

describe('RejectionMessageDialogComponent', () => {
  let component: RejectionMessageDialogComponent;
  let fixture: ComponentFixture<RejectionMessageDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RejectionMessageDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RejectionMessageDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
