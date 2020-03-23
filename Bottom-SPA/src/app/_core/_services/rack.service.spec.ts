/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RackService } from './rack.service';

describe('Service: Rack', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RackService]
    });
  });

  it('should ...', inject([RackService], (service: RackService) => {
    expect(service).toBeTruthy();
  }));
});
