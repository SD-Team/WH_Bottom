/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { QrcodeMainService } from './qrcode-main.service';

describe('Service: QrcodeMain', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [QrcodeMainService]
    });
  });

  it('should ...', inject([QrcodeMainService], (service: QrcodeMainService) => {
    expect(service).toBeTruthy();
  }));
});
