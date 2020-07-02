import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CursusListComponent } from './cursus-list.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatTableModule } from '@angular/material/table';

describe('CursusListComponent', () => {
  let component: CursusListComponent;
  let fixture: ComponentFixture<CursusListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursusListComponent ],
      imports: [
        HttpClientTestingModule,
        MatTableModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursusListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should show Overzicht title', () => {
    expect(document.querySelector('h3').textContent == 'Overzicht').toBeTruthy();
  })

  it('should show table headers', () => {
    var headers = document.querySelectorAll('.headerCell');

    expect(headers[0].textContent).toBe('Startdatum');
    expect(headers[1].textContent).toBe('Duur');
    expect(headers[2].textContent).toBe('Titel');
  })
});
