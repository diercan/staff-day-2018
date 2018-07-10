import { browser, by, element } from 'protractor';

export class ClientPage {
  navigateTo() {
    return browser.get('/');
  }

  getParagraphText() {
    return element(by.css('ui-root h1')).getText();
  }
}
