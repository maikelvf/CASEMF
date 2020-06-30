import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CursustoevoegenComponent } from './cursustoevoegen.component';

describe('CursustoevoegenComponent', () => {
  let component: CursustoevoegenComponent;
  let fixture: ComponentFixture<CursustoevoegenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursustoevoegenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursustoevoegenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
