import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { User, PointsChanges } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { input } from '@aws-amplify/ui';

@Component({
  selector: 'app-sponsor-home-page',
  templateUrl: './sponsor-home-page.component.html',
  styleUrls: ['./sponsor-home-page.component.css'],
})
export class SponsorHomePageComponent implements OnInit {
  @Input() cognitoUser: any;
  @Input() dbUser!: User;
  sponsoredDrivers!: User[];

  constructor(
    private router: Router,
    private cognitoService: CognitoService,
    private userService: UserService,
    private pointsChangesService: PointsChangesService
  ) {}

  ngOnInit(): void {
    console.log(this.dbUser);
    this.userService
      .getDriversBySponsor(this.dbUser.sponsorId)
      .subscribe((drivers) => {
        this.sponsoredDrivers = drivers;
      });
  }
}
