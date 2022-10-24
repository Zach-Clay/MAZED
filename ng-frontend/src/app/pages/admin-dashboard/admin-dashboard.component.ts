import { Component, OnInit } from '@angular/core';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { SponsorOrg } from 'src/app/models/interfaces';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent implements OnInit {
  addingAdmin: boolean = false;
  addingSponsor: boolean = false;
  orgs!: SponsorOrg[];
  orgToAddSponsor!: SponsorOrg;

  constructor(private sponsorOrgService: SponsorOrgService) {}

  ngOnInit(): void {
    //Get the orgs
    this.sponsorOrgService.getAllOrgs().subscribe((data) => {
      this.orgs = data;
    });
  }
}
