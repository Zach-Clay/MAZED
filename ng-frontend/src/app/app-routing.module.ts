import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';

//Pages
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { UserRegistrationComponent } from './pages/user-registration/user-registration.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page/profile-page.component';
import { DriverApplicationComponent } from './pages/driver-application/driver-application.component';
import { SponsorsDashboardComponent } from './pages/sponsors-dashboard/sponsors-dashboard.component';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard.component';
import { RegistrationPageComponent } from './pages/registration-page/registration-page.component';
import { EditProductCatalogComponent } from './pages/edit-product-catalog/edit-product-catalog.component';
import { ProductCatalogComponent } from './pages/product-catalog/product-catalog.component';
import { ViewCatalogComponent } from './pages/view-catalog/view-catalog.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { AuditReportsComponent } from './pages/audit-reports/audit-reports.component';
import { SponsorReportPageComponent } from './pages/sponsor-report-page/sponsor-report-page.component';

const routes: Routes = [
  { path: '', component: LoginPageComponent },
  { path: 'register', component: RegistrationPageComponent },
  { path: 'home', component: HomePageComponent },
  { path: 'profile', component: ProfilePageComponent },
  { path: 'driver-application', component: DriverApplicationComponent },
  { path: 'sponsor-dashboard', component: SponsorsDashboardComponent },
  { path: 'admin-dashboard', component: AdminDashboardComponent },
  { path: 'edit-catalog', component: EditProductCatalogComponent },
  { path: 'product-catalog', component: ProductCatalogComponent },
  { path: 'view-catalog', component: ViewCatalogComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'audit-reports', component: AuditReportsComponent },
  { path: 'sponsor-reports', component: SponsorReportPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
