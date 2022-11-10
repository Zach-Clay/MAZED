import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SponsorOrg } from 'src/app/models/interfaces';
import { SponsorOrgService } from '../../services/sponsor-org.service';

@Component({
  selector: 'app-sponsor-creation-form',
  templateUrl: './sponsor-creation-form.component.html',
  styleUrls: ['./sponsor-creation-form.component.css'],
})
export class SponsorCreationFormComponent implements OnInit {
  name: string = '';
  desc: string = '';
  d2p!: number;
  loading: boolean = false;

  constructor(private sponsorOrgService: SponsorOrgService) {}

  ngOnInit(): void {}

  onSubmit() {
    this.loading = true;
    if (!this.name || !this.desc || !this.d2p) {
      alert('Please complete the form.');
      this.loading = false;
      return;
    }

    let org: SponsorOrg = {
      id: 0,
      orgName: this.name,
      orgDescription: this.desc,
      dollarToPoint: +this.d2p,
      catalogueId: 0,
      isBlacklisted: 0,
    };

    this.sponsorOrgService.addSponsorOrg(org).subscribe();
    this.loading = false;
    alert(`${this.name} has been created!`);
    this.name = '';
    this.desc = '';
    this.d2p = 0.0;
    return;
  }
}
