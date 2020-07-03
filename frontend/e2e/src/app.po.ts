import { browser, by, element } from 'protractor';
import { __values } from 'tslib';

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

    count = element.all(by.css('.tableRow')).count().then(value => value);

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

  succesMessageVisible(): Promise<string> {
    return element(by.id('succesmessage')).getText() as Promise<string>;
  }

  noFileErrorMessageVisible(): Promise<string> {
    return element(by.id('errormessage')).getText() as Promise<string>;
  }

  fileInputElement() {
    return element(by.id('file'));
  }

  uploadFile(filePath: string) {
    const path = require('path');
    var absolutePath = path.resolve(filePath);
    this.fileInputElement().sendKeys(absolutePath);
    this.clickSubmitButton();
  }
}
