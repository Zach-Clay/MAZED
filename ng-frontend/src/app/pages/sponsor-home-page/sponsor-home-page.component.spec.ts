import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsorHomePageComponent } from './sponsor-home-page.component';

describe('SponsorHomePageComponent', () => {
  let component: SponsorHomePageComponent;
  let fixture: ComponentFixture<SponsorHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SponsorHomePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SponsorHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
