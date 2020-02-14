import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardimagesComponent } from './cardimages.component';

describe('CardimagesComponent', () => {
  let component: CardimagesComponent;
  let fixture: ComponentFixture<CardimagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardimagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardimagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
