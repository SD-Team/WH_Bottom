/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PackingListService } from './packing-list.service';

describe('Service: PackingList', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PackingListService]
    });
  });

  it('should ...', inject([PackingListService], (service: PackingListService) => {
    expect(service).toBeTruthy();
  }));
});
