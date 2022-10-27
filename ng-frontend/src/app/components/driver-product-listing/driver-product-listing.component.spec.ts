import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverProductListingComponent } from './driver-product-listing.component';

describe('DriverProductListingComponent', () => {
  let component: DriverProductListingComponent;
  let fixture: ComponentFixture<DriverProductListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DriverProductListingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DriverProductListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
