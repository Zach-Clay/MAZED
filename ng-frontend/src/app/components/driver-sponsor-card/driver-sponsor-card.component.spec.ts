import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverSponsorCardComponent } from './driver-sponsor-card.component';

describe('DriverSponsorCardComponent', () => {
  let component: DriverSponsorCardComponent;
  let fixture: ComponentFixture<DriverSponsorCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DriverSponsorCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DriverSponsorCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
