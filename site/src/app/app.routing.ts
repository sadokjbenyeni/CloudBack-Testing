import { PasswordResetComponent } from './components/login/password-reset/password-reset.component';
import { ActivationComponent } from './components/activation/activation.component';
import { Routes } from '@angular/router';

// Guards
import { GuardGuard } from './guard.guard';

// Site Admins
import { SubscriptionserrorsComponent } from './components/admin/subscriptions-errors/subscriptions-errors.component';
import { SubscriptionsComponent } from './components/admin/subscriptions/subscriptions.component';
import { SubscriptionsactiveComponent } from './components/admin/subscriptions-active/subscriptions-active.component';
import { SubscriptionsValidationComponent } from './components/admin/subscriptions-validation/subscriptions-validation.component';
import { SubscriptionsacceptedComponent } from './components/admin/subscriptions-accepted/subscriptions-accepted.component';
// Site Clients
import { SignUpComponent } from './components/signup/signup.component';
import { LoginPageComponent } from './components/login/login-page/login-page.component';
import { CryptocurrencyComponent } from './components/cryptocurrency/cryptocurrency.component';
import { MutualizedSubscriptionComponent } from './components/mutualized-subscription/mutualized-subscription.component';


import { MySubscriptionsComponent } from './components/userProfile/my-subscriptions/my-subscriptions.component';
import { AdminMenuComponent } from './components/admin/admin-menu/admin-menu.component';
import { ProfileMenuComponent } from './components/userProfile/profile-menu/profile-menu.component';
import { PaymentComponent } from './components/userProfile/PaymentsModule/payment/payment.component';
import { GeneralInformationsComponent } from './components/userProfile/general-informations/general-informations.component';
import { ChangePasswordComponent } from './components/userProfile/change-password/change-password.component';
// presentation pages
import { OnBoardingComponent } from './components/Presentationpages/on-boarding/on-boarding.component';
import { ProductsComponent } from './components/Presentationpages/products/products.component';
import { PricingComponent } from './components/Presentationpages/pricing/pricing.component';
import { AboutComponent } from './components/Presentationpages/about/about.component';
import { HelpPageComponent } from './components/Presentationpages/help-page/help-page.component';

export const routes: Routes = [
  { path: '', redirectTo: 'on-boarding', pathMatch: 'full' },

  // CLIENT
  {
    path: 'subscription', children:
      [
        { path: '', redirectTo: 'mutualized', pathMatch: 'full' },
        { path: 'mutualized', component: MutualizedSubscriptionComponent, canActivate: [GuardGuard] }
      ]
  },
  {
    path: 'profile', component: ProfileMenuComponent, children:
      [
        { path: '', redirectTo: 'informations', pathMatch: 'full' },
        { path: 'subscriptions', component: MySubscriptionsComponent, canActivate: [GuardGuard] },
        { path: 'informations', component: GeneralInformationsComponent, canActivate: [GuardGuard] },
        { path: 'payment', component: PaymentComponent, canActivate: [GuardGuard] },
        { path: 'change-password', component: ChangePasswordComponent, canActivate: [GuardGuard] },

      ]
  },

  // ADMINISTRATOR

  {
    path: 'admin', component: AdminMenuComponent, children: [
      { path: '', redirectTo: 'subscriptions', pathMatch: 'full' },
      { path: 'subscriptions-validation', component: SubscriptionsValidationComponent, canActivate: [GuardGuard], data: { roles: ["Administrator"] } },
      { path: 'subscriptions-accepted', component: SubscriptionsacceptedComponent, canActivate: [GuardGuard], data: { roles: ["Administrator"] } },
      { path: 'subscriptions-errors', component: SubscriptionserrorsComponent, canActivate: [GuardGuard], data: { roles: ["Administrator"] } },
      { path: 'subscriptions-active', component: SubscriptionsactiveComponent, canActivate: [GuardGuard], data: { roles: ["Administrator"] } },
      { path: 'subscriptions', component: SubscriptionsComponent, canActivate: [GuardGuard], data: { roles: ["Administrator"] } },
    ]
  },
  // NO LOGIN NEEDED
  { path: 'on-boarding', component: OnBoardingComponent },
  { path: 'cryptocurrency', component: CryptocurrencyComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'pricing', component: PricingComponent },
  { path: 'about', component: AboutComponent },
  { path: 'help', component: HelpPageComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'activation/:token', component: ActivationComponent },
  { path: 'mdp/:token', component: PasswordResetComponent },
  { path: '**', component: OnBoardingComponent }

];
