import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProductCatalogComponent } from './edit-product-catalog.component';

describe('EditProductCatalogComponent', () => {
  let component: EditProductCatalogComponent;
  let fixture: ComponentFixture<EditProductCatalogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditProductCatalogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditProductCatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
