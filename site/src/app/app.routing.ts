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
import { LoginComponent } from './components/login/login.component';
import { SignUpComponent } from './components/signup/signup.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch : 'full' },
  // CLIENT
  { path: 'account', component : SignUpComponent, canActivate: [GuardGuard] },
  // ADMINISTRATOR
  { path: 'admin/profil/:id', component : UserDetailComponent, canActivate: [GuardGuard] },
  { path: 'admin/users', component : UsersComponent, canActivate: [GuardGuard] },
  { path: 'admin/role', component : RoleComponent, canActivate: [GuardGuard] },
  { path: 'admin/countries', component : CountriesComponent, canActivate: [GuardGuard] },
  { path: 'admin/terms', component : TermsComponent, canActivate: [GuardGuard] },
  // ALL USERS
  { path: 'home', component : HomeComponent },
  { path: 'about', component : AboutComponent },
  { path: 'help', component : HelpPageComponent },
  { path: 'signup', component : SignUpComponent },
  { path: 'login', component : LoginComponent },
  { path: 'activation/:token', component : LoginComponent },
  { path: 'mdp/:token', component : LoginComponent },
  { path: '**', component: HomeComponent }
];
