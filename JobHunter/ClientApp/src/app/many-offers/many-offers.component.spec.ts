import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManyOffersComponent } from './many-offers.component';

describe('ManyOffersComponent', () => {
  let component: ManyOffersComponent;
  let fixture: ComponentFixture<ManyOffersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManyOffersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManyOffersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
