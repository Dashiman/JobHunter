import { TestBed } from '@angular/core/testing';

import { JobOfferService } from './job-offer.service';

describe('JobOfferService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: JobOfferService = TestBed.get(JobOfferService);
    expect(service).toBeTruthy();
  });
});
