import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsoredDriverComponent } from './sponsored-driver.component';

describe('SponsoredDriverComponent', () => {
  let component: SponsoredDriverComponent;
  let fixture: ComponentFixture<SponsoredDriverComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SponsoredDriverComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SponsoredDriverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
