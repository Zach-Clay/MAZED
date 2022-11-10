import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsorCreationFormComponent } from './sponsor-creation-form.component';

describe('SponsorCreationFormComponent', () => {
  let component: SponsorCreationFormComponent;
  let fixture: ComponentFixture<SponsorCreationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SponsorCreationFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SponsorCreationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
