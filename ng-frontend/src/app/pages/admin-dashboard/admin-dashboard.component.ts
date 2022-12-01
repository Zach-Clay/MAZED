import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/models/interfaces';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { SponsorOrg } from 'src/app/models/interfaces';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent implements OnInit {
  @Input() user!: User;
  addingAdmin: boolean = false;
  addingSponsor: boolean = false;
  addingDriver: boolean = false;
  addingSponsorOrg: boolean = false;
  orgs!: SponsorOrg[];
  orgToAddSponsor!: SponsorOrg;

  constructor(private sponsorOrgService: SponsorOrgService, private router: Router) {}

  ngOnInit(): void {
    //Get the orgs
    this.sponsorOrgService.getAllOrgs().subscribe((data) => {
      this.orgs = data;
    });
  }

  navToProductCatalog() {
    this.router.navigate(['view-catalog']);
  }
}
