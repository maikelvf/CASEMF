import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CursusListComponent } from './cursus-list.component';

describe('CursusListComponent', () => {
  let component: CursusListComponent;
  let fixture: ComponentFixture<CursusListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursusListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursusListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
