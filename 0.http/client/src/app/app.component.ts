import { Component } from '@angular/core';
import { ToasterConfig } from 'angular2-toaster';

@Component({
  selector: 'ui-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  public toastsConfiguration: ToasterConfig = new ToasterConfig({ animation: 'fade' });
}
