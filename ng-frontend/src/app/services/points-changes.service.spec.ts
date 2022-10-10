import { TestBed } from '@angular/core/testing';

import { PointsChangesService } from './points-changes.service';

describe('PointsChangesService', () => {
  let service: PointsChangesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PointsChangesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
