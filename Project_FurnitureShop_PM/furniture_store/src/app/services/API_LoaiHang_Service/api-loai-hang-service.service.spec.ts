import { TestBed } from '@angular/core/testing';

import { APILoaiHangServiceService } from './api-loai-hang-service.service';

describe('APILoaiHangServiceService', () => {
  let service: APILoaiHangServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(APILoaiHangServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
