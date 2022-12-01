import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SponsorOrg } from 'src/app/models/interfaces';
import { SponsorOrgService } from '../../services/sponsor-org.service';
import { User, PointsChanges, UserToSponsor } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';

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

  constructor(
    private sponsorOrgService: SponsorOrgService,
    private userService: UserService,
    private pointChangesService: PointsChangesService
  ) {}

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

    this.sponsorOrgService.addSponsorOrg(org).subscribe((sponsorData) => {
      var testDriver: User = {} as User;
      testDriver.id = 0;
      testDriver.username = `sponsor${sponsorData.id}_%driver`;
      testDriver.userFname = `${this.name}`;
      testDriver.userLname = 'Test Driver';
      testDriver.userType = 'driver';
      testDriver.userAddress = 'string';
      testDriver.userEmail = 'string';
      testDriver.userPwd = 'null';
      testDriver.userPhoneNum = 'string';
      testDriver.isBlacklisted = 0;
      var testSponsor: User = {} as User;
      testSponsor.id = 0;
      testSponsor.username = `sponsor${sponsorData.id}_%sponsor`;
      testSponsor.userFname = `${this.name}`;
      testSponsor.userLname = 'Test Sponsor';
      testSponsor.userType = 'sponsor';
      testSponsor.userAddress = 'string';
      testSponsor.userEmail = 'string';
      testSponsor.userPwd = 'null';
      testSponsor.userPhoneNum = 'string';
      testSponsor.isBlacklisted = 0;
      this.userService.registerUser(testDriver).subscribe((userData) => {
        let userToSponsor: UserToSponsor = {
          id: 0,
          userId: userData.id,
          sponsorId: sponsorData.id,
          userPoints: 0,
          userType: 'driver',
        };
        this.userService
          .postUserToSponsor(userToSponsor)
          .subscribe((u2sData) => {
            const pointTrans: PointsChanges = {
              pointId: 0,
              sponsorId: sponsorData.id,
              userId: userData.id,
              pointValue: 99999,
              reason: 'these points are for testing purposes',
            };
            this.pointChangesService.postTransation(pointTrans);
          });
      });
      this.userService.registerUser(testSponsor).subscribe((userData) => {
        let userToSponsor: UserToSponsor = {
          id: 0,
          userId: userData.id,
          sponsorId: sponsorData.id,
          userPoints: 0,
          userType: 'sponsor',
        };
        this.userService.postUserToSponsor(userToSponsor).subscribe();
      });
    });

    this.loading = false;
    alert(`${this.name} has been created!`);
    this.name = '';
    this.desc = '';
    this.d2p = 0.0;
    return;
  }
}
