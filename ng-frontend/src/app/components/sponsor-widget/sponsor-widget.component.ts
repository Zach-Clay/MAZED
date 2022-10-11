import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { User, SponsorOrg } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';

@Component({
  selector: 'app-sponsor-widget',
  templateUrl: './sponsor-widget.component.html',
  styleUrls: ['./sponsor-widget.component.css']
})
export class SponsorWidgetComponent implements OnInit {
  user!: User;
  showInfo: boolean = false;
  showSponsor: boolean = false;
  sponsorOrg!: SponsorOrg;

  constructor(
    private cognitoService: CognitoService,
    private userService: UserService,
    private sponsorOrgService: SponsorOrgService
    ) { }

  ngOnInit(): void {
    this.cognitoService
      .getUser()
      .then((user: any) => {
        this.userService.getUser(user.username).subscribe((data)=>{
          this.user = data;
          this.showInfo = true;
          
          //get sponsor info
          if (this.user.sponsorId !== 0) {
            this.sponsorOrgService.getSponsorOrg(this.user.sponsorId).subscribe((org) => {
              this.sponsorOrg = org;
              this.showSponsor = true;
            })
          }

        })
      })
      .catch((err) => {
        console.log(err);
      });
  }

}
