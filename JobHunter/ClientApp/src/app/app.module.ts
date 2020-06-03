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
import { OfferFormComponent } from './offer-form/offer-form.component';
import { EditOfferComponent } from './edit-offer/edit-offer.component';
import { ProfileComponent } from './profile/profile.component';
import { ManyOffersComponent } from './many-offers/many-offers.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { AtomSpinnerModule } from 'angular-epic-spinners'
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
    NewOfferComponent,
    OfferFormComponent,
    EditOfferComponent,
    ProfileComponent,
    ManyOffersComponent
  ],
  imports: [NgxPaginationModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AtomSpinnerModule,
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
      { path: 'offer/:id', component: OfferDetailsComponent, canActivate: [AuthGuard]},
      { path: 'newOffer', component: NewOfferComponent ,canActivate:[AuthGuard]},
      { path: 'editOffer', component: EditOfferComponent, canActivate: [AuthGuard] },
      { path: 'user/:id', component: ProfileComponent, canActivate: [AuthGuard] },
    ])
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
