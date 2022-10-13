import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverApplicationComponent } from './driver-application.component';

describe('DriverApplicationComponent', () => {
  let component: DriverApplicationComponent;
  let fixture: ComponentFixture<DriverApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DriverApplicationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DriverApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
