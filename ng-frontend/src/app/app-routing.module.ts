import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';

//Pages
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { UserRegistrationComponent } from './pages/user-registration/user-registration.component';

const routes: Routes = [
  { path: '', component: LoginPageComponent },
  { path: 'register', component: UserRegistrationComponent },
  { path: 'home', component: HomePageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
