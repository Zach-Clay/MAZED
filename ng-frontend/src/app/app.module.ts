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
import { SponsorsDashboardComponent } from './pages/sponsors-dashboard/sponsors-dashboard.component';
import { DriverHomePageComponent } from './pages/driver-home-page/driver-home-page.component';
import { SponsorHomePageComponent } from './pages/sponsor-home-page/sponsor-home-page.component';
import { AddDeductDialog } from './pages/sponsors-dashboard/sponsors-dashboard.component';
import { ProductListingComponent } from './components/product-listing/product-listing.component';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard.component';
import { RegistrationPageComponent } from './pages/registration-page/registration-page.component';
import { AdminHomepageComponent } from './pages/admin-homepage/admin-homepage.component';
import { DriverSponsorCardComponent } from './components/driver-sponsor-card/driver-sponsor-card.component';
import { DriverProductListingComponent } from './components/driver-product-listing/driver-product-listing.component';
import { EditProductCatalogComponent } from './pages/edit-product-catalog/edit-product-catalog.component';
import { ProductCatalogComponent } from './pages/product-catalog/product-catalog.component';
import { SponsorCreationFormComponent } from './components/sponsor-creation-form/sponsor-creation-form.component';
import { ViewCatalogComponent } from './pages/view-catalog/view-catalog.component';

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
    SponsorsDashboardComponent,
    DriverHomePageComponent,
    SponsorHomePageComponent,
    AddDeductDialog,
    ProductListingComponent,
    AdminDashboardComponent,
    RegistrationPageComponent,
    AdminHomepageComponent,
    DriverSponsorCardComponent,
    DriverProductListingComponent,
    EditProductCatalogComponent,
    ProductCatalogComponent,
    SponsorCreationFormComponent,
    ViewCatalogComponent,
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
