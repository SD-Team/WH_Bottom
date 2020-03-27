/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PackingListDetailService } from './packing-list-detail.service';

describe('Service: PackingListDetail', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PackingListDetailService]
    });
  });

  it('should ...', inject([PackingListDetailService], (service: PackingListDetailService) => {
    expect(service).toBeTruthy();
  }));
});
