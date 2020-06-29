import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CursusoverzichtComponent } from './cursusoverzicht.component';

describe('CursusoverzichtComponent', () => {
  let component: CursusoverzichtComponent;
  let fixture: ComponentFixture<CursusoverzichtComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursusoverzichtComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursusoverzichtComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
