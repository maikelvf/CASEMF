import { browser, by, element } from 'protractor';
import { __values } from 'tslib';
import { stringify } from 'querystring';

export class AppPage {
  navigateTo(): Promise<unknown> {
    return browser.get(browser.baseUrl) as Promise<unknown>;
  }

  navigateToRoute(route): Promise<unknown> {
    return browser.get(route) as Promise<unknown>;
  }

  getTitleText(): Promise<string> {
    return element(by.id('cursusadministratie')).getText() as Promise<string>;
  }

  clickSubmitButton(): void {
    element(by.id('submitButton')).click();
  }

  getNumberOfCursussen(): Number {
    var count;

    count = element.all(by.css('.tableRow')).count();

    return count;
  }

  clickIncreaseWeeknumberButton(n) {
    for (var i = 0; i < n; i++) {
      element(by.id('increaseButton')).click();
    }
  }

  clickDecreaseWeeknumberButton(n) {
    for (var i = 0; i < n; i++) {
      element(by.id('decreaseButton')).click();
    }
  }

  noFileErrorMessageVisible(): string {
    var text;

    element(by.id('errormessage')).getText().then(value => { text = value });

    return text;
  }
}
