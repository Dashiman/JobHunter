import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferlistComponent } from './offerlist.component';

describe('OfferlistComponent', () => {
  let component: OfferlistComponent;
  let fixture: ComponentFixture<OfferlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OfferlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OfferlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
