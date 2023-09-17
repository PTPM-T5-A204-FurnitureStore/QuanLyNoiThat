import { TestBed } from '@angular/core/testing';

import { APISanPhamServiceService } from './api-san-pham-service.service';

describe('APISanPhamServiceService', () => {
  let service: APISanPhamServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(APISanPhamServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
