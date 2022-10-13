import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PointChangeComponent } from './point-change.component';

describe('PointChangeComponent', () => {
  let component: PointChangeComponent;
  let fixture: ComponentFixture<PointChangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PointChangeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PointChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
