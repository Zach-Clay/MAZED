import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsorsDashboardComponent } from './sponsors-dashboard.component';

describe('SponsorsDashboardComponent', () => {
  let component: SponsorsDashboardComponent;
  let fixture: ComponentFixture<SponsorsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SponsorsDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SponsorsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
