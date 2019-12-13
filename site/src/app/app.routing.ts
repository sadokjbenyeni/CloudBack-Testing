import { PasswordResetComponent } from './components/login/password-reset/password-reset.component';
import { ActivationComponent } from './components/activation/activation.component';
import { Routes } from '@angular/router';

// Guards
import { GuardGuard } from './guard.guard';

// Site Admins
import { UsersComponent } from './components/admin/users/users.component';
import { UserDetailComponent } from './components/admin/user-detail/user-detail.component';
import { RoleComponent } from './components/admin/role/role.component';
import { CountriesComponent } from './components/admin/countries/countries.component';
import { TermsComponent } from './components/admin/terms/terms.component';

// Site Clients
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from './components/about/about.component';
import { HelpPageComponent } from './components/help-page/help-page.component';
import { SignUpComponent } from './components/signup/signup.component';
import { ProductsComponent } from './components/products/products.component';
import { PricingComponent } from './components/pricing/pricing.component';
import { LoginPageComponent } from './components/login/login-page/login-page.component';
import { CryptocurrencyComponent } from './components/cryptocurrency/cryptocurrency.component';
import { OnBoardingComponent } from './components/on-boarding/on-boarding.component';
import { MutualizedSubscriptionComponent } from './components/mutualized-subscription/mutualized-subscription.component';
import { SubscriptionsValidationComponent } from './components/subscriptions-validation/subscriptions-validation.component';
import { SubscriptionsacceptedComponent } from './components/subscriptions-accepted/subscriptions-accepted.component';

export const routes: Routes = [
  { path: '', redirectTo: '/on-boarding', pathMatch: 'full' },

  // CLIENT
  { path: 'account', component: SignUpComponent, canActivate: [GuardGuard] },
  { path: 'subscription/mutualized', component: MutualizedSubscriptionComponent, canActivate: [GuardGuard] },
  // ADMINISTRATOR
  { path: 'admin/profil/:id', component: UserDetailComponent, canActivate: [GuardGuard] },
  { path: 'subscriptions-validation', component: SubscriptionsValidationComponent, canActivate: [GuardGuard] },
  { path: 'Subscriptions-accepted', component: SubscriptionsacceptedComponent, canActivate: [GuardGuard] },

  { path: 'admin/users', component: UsersComponent, canActivate: [GuardGuard] },
  { path: 'admin/role', component: RoleComponent, canActivate: [GuardGuard] },
  { path: 'admin/countries', component: CountriesComponent, canActivate: [GuardGuard] },
  { path: 'admin/terms', component: TermsComponent, canActivate: [GuardGuard] },

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
