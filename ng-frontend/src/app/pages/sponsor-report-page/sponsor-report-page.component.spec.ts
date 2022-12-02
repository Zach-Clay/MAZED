import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsorReportPageComponent } from './sponsor-report-page.component';

describe('SponsorReportPageComponent', () => {
  let component: SponsorReportPageComponent;
  let fixture: ComponentFixture<SponsorReportPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SponsorReportPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SponsorReportPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
