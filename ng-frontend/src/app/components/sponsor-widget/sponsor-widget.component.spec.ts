import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsorWidgetComponent } from './sponsor-widget.component';

describe('SponsorWidgetComponent', () => {
  let component: SponsorWidgetComponent;
  let fixture: ComponentFixture<SponsorWidgetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SponsorWidgetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SponsorWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
