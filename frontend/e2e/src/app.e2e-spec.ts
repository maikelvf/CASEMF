import { AppPage } from './app.po';
import { browser, logging } from 'protractor';

  // LET OP: Onderstaande testen zijn afhankelijk van de initiële staat van de database. Oftewel:
  // Backend applicatie moet draaiende zijn, update-database moet zijn gedraaid,
  // en de gebruiker heeft nog geen nieuwe cursussen toegevoegd of deze testen al eens gedraaid.
  // In een volgende iteratie van deze tests zou de database gemocked kunnen worden.

describe('workspace-project App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display Cursusadministratie', () => {
    page.navigateTo();
    expect(page.getTitleText()).toEqual('Cursusadministratie');
  });

  it('should display correct number of cursussen when switching weeknumbers', () => {
    page.navigateTo();
    expect(page.getNumberOfCursussen()).toBe(6);

    page.clickIncreaseWeeknumberButton(1);
    expect(page.getNumberOfCursussen()).toBe(2);

    page.clickDecreaseWeeknumberButton(2);
    expect(page.getNumberOfCursussen()).toBe(4);
  });

  it(`should display succes messages after uploading valid file`, () => {
    page.navigateToRoute('/toevoegen');
    page.uploadFile('./testfiles/validFile1.txt');
    expect(page.succesMessageVisible()).toEqual('1 nieuwe cursus(sen) toegevoegd, 5 nieuwe instantie(s) toegevoegd');
  });

  it(`should display 'no file selected' error when trying to upload with no file selected`, () => {
    page.navigateToRoute('/toevoegen');
    page.clickSubmitButton();
    expect(page.noFileErrorMessageVisible()).toEqual('Kies een bestand om te uploaden!');
  });

  afterEach(async () => {
    // Assert that there are no errors emitted from the browser
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining({
      level: logging.Level.SEVERE,
    } as logging.Entry));
  });
});
