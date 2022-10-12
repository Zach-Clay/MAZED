import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import {} from 'aws-amplify';
import { HttpClientModule } from '@angular/common/http';

//Angular Material
import { AngularMaterialModule } from './angular-material.module';

//Components
import { UserRegistrationComponent } from './pages/user-registration/user-registration.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { HeaderComponent } from './components/header/header.component';
import { LoginComponent } from './components/login/login.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { HomePageHeaderComponent } from './components/home-page-header/home-page-header.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page/profile-page.component';
import {
  SponsorWidgetComponent,
  SponsorAppDialog,
} from './components/sponsor-widget/sponsor-widget.component';
import { DriverApplicationComponent } from './pages/driver-application/driver-application.component';
import { PointChangeComponent } from './components/point-change/point-change.component';
import { SponsoredDriverComponent } from './components/sponsored-driver/sponsored-driver.component';
import { DriverHomePageComponent } from './pages/driver-home-page/driver-home-page.component';
import { SponsorHomePageComponent } from './pages/sponsor-home-page/sponsor-home-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    UserRegistrationComponent,
    LoginPageComponent,
    HomePageComponent,
    HomePageHeaderComponent,
    ProfilePageComponent,
    SponsorWidgetComponent,
    DriverApplicationComponent,
    SponsorAppDialog,
    PointChangeComponent,
    SponsoredDriverComponent,
    DriverHomePageComponent,
    SponsorHomePageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
