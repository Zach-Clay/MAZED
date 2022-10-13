import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';

//Pages
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { UserRegistrationComponent } from './pages/user-registration/user-registration.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page/profile-page.component';
import { DriverApplicationComponent } from './pages/driver-application/driver-application.component';
import { SponsorsDashboardComponent } from './pages/sponsors-dashboard/sponsors-dashboard.component';

const routes: Routes = [
  { path: '', component: LoginPageComponent },
  { path: 'register', component: UserRegistrationComponent },
  { path: 'home', component: HomePageComponent },
  { path: 'profile', component: ProfilePageComponent },
  { path: 'driver-application', component: DriverApplicationComponent },
  { path: 'sponsor-dashboard', component: SponsorsDashboardComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
