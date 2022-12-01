import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/models/interfaces';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { SponsorOrg } from 'src/app/models/interfaces';

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
  viewingReports: boolean = false;

  constructor(private sponsorOrgService: SponsorOrgService) {}

  ngOnInit(): void {
    //Get the orgs
    this.sponsorOrgService.getAllOrgs().subscribe((data) => {
      this.orgs = data;
    });
  }
}
