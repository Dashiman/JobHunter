import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  constructor(private translate: TranslateService) {

    translate.setDefaultLang('pl');
  }
  useLanguage(language: string) {
    this.translate.use(language);
  }
}
