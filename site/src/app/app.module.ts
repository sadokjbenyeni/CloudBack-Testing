import { LoginService } from './services/login.service';
import { AuthentificationService } from './services/authentification.service';
import { MaterialModule } from './components/material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { routes } from './app.routing';
// Guard
import { GuardGuard } from './guard.guard';
// Validate Form
import { ShowErrorsComponent } from './show-errors.component';
import { EmailValidatorDirective } from './validators/email-validator.directive';
import { EqualValidatorDirective } from './validators/equal-validator.directive';
import { TelephoneValidatorDirective } from './validators/telephone-validator.directive';
import { MdpValidatorDirective } from './validators/mdp-validator.directive';
import { DateValidatorDirective } from './validators/date-validator.directive';
// Components
// All
import { MenuComponent } from './components/menu/menu.component';
// Clients
import { AppComponent } from './app.component';
import { LoginDialogComponent } from './components/login/login-dialog/login-dialog.component';
import { SignUpComponent } from './components/signup/signup.component';

// Administrators
import { ConfigurationComponent } from './components/admin/configuration/configuration.component';
import { RoleComponent } from './components/admin/role/role.component';
import { CountriesComponent } from './components/admin/countries/countries.component';
import { TermsComponent } from './components/admin/terms/terms.component';
import { UsersComponent } from './components/admin/users/users.component';
import { UserDetailComponent } from './components/admin/user-detail/user-detail.component';

// Services
import { DataService } from './data.service';
import { CurrencyService } from './services/currency.service';
import { RoleService } from './services/role.service';
import { UserService } from './services/user.service';
import { CountriesService } from './services/countries.service';
import { CompanytypesService } from './services/companytypes.service';
import { ComplianceService } from './services/compliance.service';
import { ConfigService } from './services/config.service';

// import { FilterPipe } from './services/callback.pipe';

// Commun
import { ComCountriesComponent } from './components/commun/com-countries/com-countries.component';

// Pipe
import { CeilPipe } from './ceil.pipe';
import { SafeHtmlPipePipe } from './safe-html-pipe.pipe';

//
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Middleware
import { RecaptchaModule } from 'ng-recaptcha';
import { DataTablesModule, DataTableDirective } from 'angular-datatables';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { NgbdModalContent } from './modal-content';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { CdkTableModule } from '@angular/cdk/table';
import { TermsOfUseComponent } from './components/terms-of-use/terms-of-use.component';
import { LoginPageComponent } from './components/login/login-page/login-page.component';
import { HomeComponent } from './components/home/home.component';
import { PricingComponent } from './components/pricing/pricing.component';
import { ProductsComponent } from './components/products/products.component';
import { ActivationComponent } from './components/activation/activation.component';
import { AboutComponent } from './components/about/about.component';
import { HelpPageComponent } from './components/help-page/help-page.component';
import { CryptocurrencyComponent } from './components/cryptocurrency/cryptocurrency.component';
import { OnBoardingComponent } from './components/on-boarding/on-boarding.component';

import { PasswordComponent } from './components/login/password/password.component';
import { PasswordResetComponent } from './components/login/password-reset/password-reset.component';
// import { PdfComponent } from './components/commun/pdf/pdf.component';
import { NotifierModule } from "angular-notifier";
import { MutualizedSubscriptionComponent } from './components/mutualized-subscription/mutualized-subscription.component';
import { SubscriptionsComponent } from './components/subscriptions/subscriptions.component';
import { SubscriptionService } from './services/subscription.service';
import { RejectionMessageDialogComponent } from './components/rejection-message-dialog/rejection-message-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginDialogComponent,
    SignUpComponent,
    MenuComponent,
    LoginPageComponent,
    HomeComponent,
    PricingComponent,
    CryptocurrencyComponent,
    OnBoardingComponent,
    ProductsComponent,
    AboutComponent,
    HelpPageComponent,
    UsersComponent,
    UserDetailComponent,
    ShowErrorsComponent,
    EmailValidatorDirective,
    EqualValidatorDirective,
    TelephoneValidatorDirective,
    MdpValidatorDirective,
    DateValidatorDirective,
    ComCountriesComponent,
    ConfigurationComponent,
    RoleComponent,
    CountriesComponent,
    TermsComponent,

    CeilPipe,
    SafeHtmlPipePipe,
    NgbdModalContent,
    ComCountriesComponent,
    TermsOfUseComponent,
    LoginPageComponent,
    ActivationComponent,
    PasswordComponent,
    PasswordResetComponent,
    MutualizedSubscriptionComponent,
    SubscriptionsComponent,
    RejectionMessageDialogComponent,

  ],
  entryComponents: [NgbdModalContent, LoginDialogComponent, PasswordComponent, PasswordResetComponent, TermsOfUseComponent, RejectionMessageDialogComponent],
  imports: [
    MaterialModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    DataTablesModule,
    CdkTableModule,
    BrowserAnimationsModule,
    // NgbModule.forRoot(),
    NgbModule,
    ReactiveFormsModule,
    //Ng2BootstrapModule,
    RecaptchaModule.forRoot(),
    PdfViewerModule,
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    NotifierModule.withConfig({}),
  ],
  providers: [
    GuardGuard,
    ConfigService,
    DataService,
    CurrencyService,
    RoleService,
    UserService,
    CountriesService,
    ComplianceService,
    CompanytypesService,
    AuthentificationService,
    LoginService,
    SubscriptionService,
    // FilterPipe
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
