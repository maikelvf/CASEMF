import { AppPage } from './app.po';
import { browser, logging } from 'protractor';

describe('workspace-project App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display Cursusadministratie', () => {
    page.navigateTo();
    expect(page.getTitleText()).toEqual('Cursusadministratie');
  });


  // LET OP: Onderstaande test is afhankelijk van de initiële database staat. Oftewel:
  // Backend applicatie moet draaiende zijn
  // Frontend applicatie opent bij week 27 jaar 2020, 4 cursussen zijn zichtbaar.
  // Weeknummer wordt door de test verandert in 28, 6 cursussen zijn zichtbaar.
  // Weeknummer wordt door de test verander in 26, 2 cursussen zijn zichtbaar.
  // Zijn er door de gebruiker cursussen toegevoegd dan zal het aantal waarschijnlijk anders zijn.

  it('should display correct number of cursussen when switching weeknumbers', () => {
    page.navigateTo();
    expect(page.getNumberOfCursussen()).toBe(4);

    page.clickIncreaseWeeknumberButton(1);
    expect(page.getNumberOfCursussen()).toBe(6);

    page.clickDecreaseWeeknumberButton(2);
    expect(page.getNumberOfCursussen()).toBe(2);
  })

  it(`should display 'no file selected' error when trying to upload with no file selected`, () => {
    page.navigateToRoute('/toevoegen');
    page.clickSubmitButton();
    expect(page.noFileErrorMessageVisible());
  });

  afterEach(async () => {
    // Assert that there are no errors emitted from the browser
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining({
      level: logging.Level.SEVERE,
    } as logging.Entry));
  });
});
