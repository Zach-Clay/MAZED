import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverProductPageComponent } from './driver-product-page.component';

describe('DriverProductPageComponent', () => {
  let component: DriverProductPageComponent;
  let fixture: ComponentFixture<DriverProductPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DriverProductPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DriverProductPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
