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
import { AboutComponent } from './components/about/about.component';
import { HelpPageComponent } from './components/help-page/help-page.component';
import { SignUpComponent } from './components/signup/signup.component';
import { ProductsComponent } from './components/products/products.component';
import { PricingComponent } from './components/pricing/pricing.component';
import { LoginPageComponent } from './components/login/login-page/login-page.component';
import { CryptocurrencyComponent } from './components/cryptocurrency/cryptocurrency.component';
import { OnBoardingComponent } from './components/on-boarding/on-boarding.component';
import { MutualizedSubscriptionComponent } from './components/mutualized-subscription/mutualized-subscription.component';

import { PaymentComponent } from './components/payment/payment.component';

import { MySubscriptionsComponent } from './components/my-subscriptions/my-subscriptions.component';
import { AdminMenuComponent } from './components/admin/admin-menu/admin-menu.component';

export const routes: Routes = [
  { path: '', redirectTo: 'on-boarding', pathMatch: 'full' },

  // CLIENT
  { path: 'account', component: SignUpComponent, canActivate: [GuardGuard], data: { roles: ["Administrator", "Client"] } },
  {
    path: 'subscription', children:
      [
        { path: '', redirectTo: 'mutualized', pathMatch: 'full' },
        { path: 'mutualized', component: MutualizedSubscriptionComponent, canActivate: [GuardGuard], data: { roles: ["Administrator", "Client"] } }
      ]
  },
  { path: 'payment', component: PaymentComponent, canActivate: [GuardGuard], data: { roles: ["Administrator", "Client"] } },
  { path: 'my-subscriptions', component: MySubscriptionsComponent, canActivate: [GuardGuard], data: { roles: ["Administrator", "Client"] } },


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
