import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { OfferlistComponent } from './offerlist/offerlist.component';
import { OfferDetailsComponent } from './offer-details/offer-details.component';
import { AuthGuardService as AuthGuard} from "./auth";
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { NewOfferComponent } from './new-offer/new-offer.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    OfferlistComponent,
    OfferDetailsComponent,
    AdminPanelComponent,
    NewOfferComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),

    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'listOfOffers', component: OfferlistComponent },
      { path: 'newUser', component: RegisterComponent },
      { path: 'signIn', component: LoginComponent },
      { path: 'offer/:id', component: OfferDetailsComponent },
      { path: 'newOffer', component: NewOfferComponent },
    ])
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
