import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { User, PointsChanges } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  isAuthenticated: boolean;
  cognitoUser: any;
  dbUser!: User;
  ogUser!: User;
  canSeeSwitchToDriver: boolean = false;
  canSeeSwitchToSponsor: boolean = false;
  displayName: boolean = false;
  isDriver: boolean = false;
  isSponsor: boolean = false;
  isAdmin: boolean = false;

  constructor(
    private router: Router,
    private cognitoService: CognitoService,
    private userService: UserService,
    private pointsChangesService: PointsChangesService
  ) {
    this.isAuthenticated = false;
  }

  ngOnInit(): void {
    this.cognitoService.isAuthenticated().then((success: boolean) => {
      this.isAuthenticated = success;
      if (!this.isAuthenticated) {
        this.router.navigate(['/']);
      } else {
        this.cognitoUser = this.cognitoService
          .getUser()
          .then((user: any) => {
            this.cognitoUser = user;
            this.userService
              .getUser(this.cognitoUser.username)
              .subscribe((data) => {
                this.ogUser = data;
                this.dbUser = data;
                this.canSeeSwitchToDriver =
                  this.ogUser.userType.toLowerCase() == 'sponsor';
                this.displayName = true;
                //determine userType
                if (this.dbUser.userType.toLowerCase() === 'driver') {
                  this.isDriver = true;
                }
                if (this.dbUser.userType.toLowerCase() === 'sponsor') {
                  this.isSponsor = true;
                }
                if (this.dbUser.userType.toLowerCase() === 'admin') {
                  this.isAdmin = true;
                }
              });
          })
          .catch((e) => {
            console.log(e);
          });
      }
    });
  }

  getPointsPerSponsor() {}

  signOut() {
    this.cognitoService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }

  switchToDriver() {
    let sponsorOrg = null;
    this.userService
      .getSponsorOrgBySponsorUserId(this.ogUser.id)
      .subscribe((org) => {
        sponsorOrg = org;
        this.userService
          .getUser(`sponsor${org.id}_%driver`)
          .subscribe((testUser) => {
            this.dbUser = testUser;
            this.isDriver = true;
            this.isSponsor = false;
            this.canSeeSwitchToSponsor = true;
            this.canSeeSwitchToDriver = false;
          });
      });
  }

  switchToSponsor() {
    this.dbUser = this.ogUser;
    this.isDriver = false;
    this.isSponsor = true;
    this.canSeeSwitchToDriver = true;
    this.canSeeSwitchToSponsor = false;
  }
}
