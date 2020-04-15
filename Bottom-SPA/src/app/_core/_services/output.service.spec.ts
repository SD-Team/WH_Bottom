/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { OutputService } from './output.service';

describe('Service: Output', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OutputService]
    });
  });

  it('should ...', inject([OutputService], (service: OutputService) => {
    expect(service).toBeTruthy();
  }));
});
