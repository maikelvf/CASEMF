import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CursustoevoegenComponent } from './cursustoevoegen.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CursustoevoegenComponent', () => {
  let component: CursustoevoegenComponent;
  let fixture: ComponentFixture<CursustoevoegenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursustoevoegenComponent ],
      imports: [HttpClientTestingModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursustoevoegenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should show Cursussen toevoegen title', () => {
    expect(document.querySelector('h3').textContent).toBe('Cursussen toevoegen');
  });

  it('should not show succes or error messages when no file uploaded yet', () => {
    expect(document.getElementById('succesmessage').textContent).toBeFalsy();
    expect(document.getElementById('duplicatemessage').textContent).toBeFalsy();
    expect(document.getElementById('errormessage').textContent).toBeFalsy();
  });
  
  it('should show errormessage when no file selected and "Toevoegen" clicked', () => {
    document.getElementById('submitButton').click();

    fixture.detectChanges();

    expect(document.getElementById('errormessage').textContent).toBe('Kies een bestand om te uploaden!');
  });
});
