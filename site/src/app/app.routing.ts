import { PasswordResetComponent } from './components/login/password-reset/password-reset.component';
import { ActivationComponent } from './components/signup/activation/activation.component';
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
import { OurMissionComponent } from './components/our-mission/our-mission.component';
import { ProductsComponent } from './components/products/products.component';
import { PricingComponent } from './components/pricing/pricing.component';
import { Component } from '@angular/core';
import { LoginDialogComponent } from './components/login/login-dialog/login-dialog.component';
import { LoginPageComponent } from './components/login/login-page/login-page.component';
import { CryptocurrencyComponent } from './components/cryptocurrency/cryptocurrency.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },

  // CLIENT
  { path: 'account', component: SignUpComponent, canActivate: [GuardGuard] },

  // ADMINISTRATOR
  { path: 'admin/profil/:id', component: UserDetailComponent, canActivate: [GuardGuard] },
  { path: 'admin/users', component: UsersComponent, canActivate: [GuardGuard] },
  { path: 'admin/role', component: RoleComponent, canActivate: [GuardGuard] },
  { path: 'admin/countries', component: CountriesComponent, canActivate: [GuardGuard] },
  { path: 'admin/terms', component: TermsComponent, canActivate: [GuardGuard] },
  // NO LOGIN NEEDED
  { path: 'cryptocurrency', component: CryptocurrencyComponent },
  { path: 'our-missions', component: OurMissionComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'pricing', component: PricingComponent },
  { path: 'about', component: AboutComponent },
  { path: 'help', component: HelpPageComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'home', component: HomeComponent },
  { path: 'activation/:token', component: ActivationComponent },
  { path: 'mdp/:token', component: PasswordResetComponent },
  { path: '**', component: HomeComponent }

];
