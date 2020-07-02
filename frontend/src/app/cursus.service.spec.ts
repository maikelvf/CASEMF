import { TestBed } from '@angular/core/testing';

import CursusService from './cursus.service';

describe('CursusService', () => {
  let service: CursusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CursusService);
  });
});
