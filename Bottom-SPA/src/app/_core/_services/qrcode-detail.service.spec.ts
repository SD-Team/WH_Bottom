/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { QrcodeDetailService } from './qrcode-detail.service';

describe('Service: QrcodeDetail', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [QrcodeDetailService]
    });
  });

  it('should ...', inject([QrcodeDetailService], (service: QrcodeDetailService) => {
    expect(service).toBeTruthy();
  }));
});
