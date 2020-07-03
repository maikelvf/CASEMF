import { TestBed } from '@angular/core/testing';

import { WeeknumberService } from './weeknumber.service';

describe('WeeknumberService', () => {
  let service: WeeknumberService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WeeknumberService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
