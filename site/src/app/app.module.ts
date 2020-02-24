import { AuthentificationService } from './services/authentification.service';
import { MaterialModule } from './components/material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { SubscriptionsacceptedComponent } from './components/admin/subscriptions-accepted/subscriptions-accepted.component';
import { SubscriptionsComponent } from './components/admin/subscriptions/subscriptions.component';
import { SubscriptionserrorsComponent } from './components/admin/subscriptions-errors/subscriptions-errors.component';
import { SubscriptionsactiveComponent } from './components/admin/subscriptions-active/subscriptions-active.component';
import { SubscriptionsValidationComponent } from './components/admin/subscriptions-validation/subscriptions-validation.component';

// Services
import { DataService } from './data.service';

import { UserService } from './services/user.service';
import { CountriesService } from './services/countries.service';
import { CompanytypesService } from './services/companytypes.service';

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
import { CdkTableModule } from '@angular/cdk/table';
import { TermsOfUseComponent } from './components/terms-of-use/terms-of-use.component';
import { LoginPageComponent } from './components/login/login-page/login-page.component';

import { ActivationComponent } from './components/activation/activation.component';
import { CryptocurrencyComponent } from './components/cryptocurrency/cryptocurrency.component';

import { PasswordResetComponent } from './components/login/password-reset/password-reset.component';
import { NotifierModule } from "angular-notifier";
import { MutualizedSubscriptionComponent } from './components/mutualized-subscription/mutualized-subscription.component';
import { AuthInterceptor } from './services/Interceptors/auth.interceptor';
import { ErrorInterceptor } from './services/Interceptors/Error.Interceptor'

import { SubscriptionConfigurationPopupComponent } from './components/subscription-configuration-popup/subscription-configuration-popup';

import { SubscriptionService } from './services/subscription.service';
import { RejectionMessageDialogComponent } from './components/rejection-message-dialog/rejection-message-dialog.component';
import { ConfirmationPopupComponent } from './components/confirmation-popup/confirmation-popup.component';

import { EnumToArrayPipe } from './EnumToList';
import { PaymentService } from './services/payment.service';

import { DatePipe } from '@angular/common';
import { SubscriptionsDetailsComponent } from './components/subscriptions-details/subscriptions-details.component';
import { CardimagesComponent } from './components/cardimages/cardimages.component';
import { AdminMenuComponent } from './components/admin/admin-menu/admin-menu.component';
import { ErrorHandlerComponent } from './components/error-handler/error-handler.component';
//user profile pages
import { MySubscriptionsComponent } from './components/userProfile/my-subscriptions/my-subscriptions.component';
import { GeneralInformationsComponent } from './components/userProfile/general-informations/general-informations.component';
import { ProfileMenuComponent } from './components/userProfile/profile-menu/profile-menu.component';
// user profile and mutualized subscription page ( under profile pages folder)
import { GetPaymentCardsComponent } from './components/userProfile/PaymentsModule/get-payment-cards/get-payment-cards.component';
import { AddPaymentCardComponent } from './components/userProfile/PaymentsModule/add-payment-card/add-payment-card.component';
import { PaymentComponent } from './components/userProfile/PaymentsModule/payment/payment.component';
import { PasswordResetEmailComponent } from './components/login/password-reset-email/password-reset-email.component';
import { ChangePasswordComponent } from './components/userProfile/change-password/change-password.component';
//presentation pages
import { HomeComponent } from './components/Presentationpages/home/home.component';
import { PricingComponent } from './components/Presentationpages/pricing/pricing.component';
import { ProductsComponent } from './components/Presentationpages/products/products.component';
import { OnBoardingComponent } from './components/Presentationpages/on-boarding/on-boarding.component';
import { AboutComponent } from './components/Presentationpages/about/about.component';
import { HelpPageComponent } from './components/Presentationpages/help-page/help-page.component';

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
    ShowErrorsComponent,
    EmailValidatorDirective,
    EqualValidatorDirective,
    TelephoneValidatorDirective,
    MdpValidatorDirective,
    DateValidatorDirective,
    ComCountriesComponent,
    CeilPipe,
    EnumToArrayPipe,
    SafeHtmlPipePipe,
    NgbdModalContent,
    ComCountriesComponent,
    TermsOfUseComponent,
    LoginPageComponent,
    ActivationComponent,
    PasswordResetEmailComponent,
    PasswordResetComponent,
    MutualizedSubscriptionComponent,
    SubscriptionsValidationComponent,
    RejectionMessageDialogComponent,
    ConfirmationPopupComponent,
    SubscriptionsacceptedComponent,
    SubscriptionsComponent,
    SubscriptionserrorsComponent,
    SubscriptionsValidationComponent,
    SubscriptionsactiveComponent,
    SubscriptionConfigurationPopupComponent,
    MySubscriptionsComponent,
    GetPaymentCardsComponent,
    AddPaymentCardComponent,
    PaymentComponent,
    SubscriptionsDetailsComponent,
    CardimagesComponent,
    AdminMenuComponent,
    ErrorHandlerComponent,
    GeneralInformationsComponent,
    ProfileMenuComponent,
    ChangePasswordComponent

  ],
  entryComponents: [NgbdModalContent, LoginDialogComponent, PasswordResetEmailComponent, PasswordResetComponent, TermsOfUseComponent, RejectionMessageDialogComponent, ConfirmationPopupComponent, SubscriptionConfigurationPopupComponent, ErrorHandlerComponent],
  imports: [
    MaterialModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    DataTablesModule,
    CdkTableModule,
    BrowserAnimationsModule,
    NgbModule,
    ReactiveFormsModule,
    RecaptchaModule.forRoot(),
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    NotifierModule.withConfig({}),
  ],
  providers: [
    GuardGuard,
    DataService,
    UserService,
    CountriesService,
    CompanytypesService,
    AuthentificationService,
    SubscriptionService,
    PaymentService,
    DatePipe,
    //middleware
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
