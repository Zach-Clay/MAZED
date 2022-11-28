import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { User, PointsChanges, SponsorOrg } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { NONE_TYPE } from '@angular/compiler';

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
  canSeeSwitchToOriginal: boolean = false;
  selectDriver: boolean = false;
  selectSponsor: boolean = false;
  sponsorSelection!: SponsorOrg;
  allOrgs!: any;
  displayName: boolean = false;
  isDriver: boolean = false;
  isSponsor: boolean = false;
  isAdmin: boolean = false;

  constructor(
    private router: Router,
    private cognitoService: CognitoService,
    private userService: UserService,
    private pointsChangesService: PointsChangesService,
    private sponsorOrgService: SponsorOrgService
  ) {
    this.isAuthenticated = false;
  }

  ngOnInit(): void {
    this.cognitoService.isAuthenticated().then((success: boolean) => {
      this.isAuthenticated = success;
      if (!this.isAuthenticated) {
        this.router.navigate(['/']);
      } else {
        this.sponsorOrgService.getAllOrgs().subscribe((data) => {
          this.allOrgs = data;
        });
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
                  this.ogUser.userType.toLowerCase() == 'sponsor' ||
                  this.ogUser.userType.toLowerCase() == 'admin';
                this.canSeeSwitchToSponsor =
                  this.ogUser.userType.toLowerCase() == 'admin';
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

  switchToDriver(showSelection: boolean) {
    if (showSelection) {
      this.selectDriver = true;
      return;
    }
    if (this.ogUser.userType.toLowerCase() == 'admin') {
      this.selectDriver = false;
      console.log('hello');

      this.userService
        .getUser(`sponsor${this.sponsorSelection.id}_%driver`)
        .subscribe((testUser) => {
          this.dbUser = testUser;
          this.isDriver = true;
          this.isAdmin = false;
          this.canSeeSwitchToOriginal = true;
          this.canSeeSwitchToDriver = false;
          this.canSeeSwitchToSponsor = false;
        });
    } else {
      this.userService
        .getSponsorOrgBySponsorUserId(this.ogUser.id)
        .subscribe((org) => {
          this.userService
            .getUser(`sponsor${org.id}_%driver`)
            .subscribe((testUser) => {
              this.dbUser = testUser;
              this.isDriver = true;
              this.isSponsor = false;
              this.canSeeSwitchToOriginal = true;
              this.canSeeSwitchToDriver = false;
            });
        });
    }
  }

  switchToSponsor(showSponsorSelection: boolean) {
    if (showSponsorSelection) {
      this.selectSponsor = true;
      return;
    }

    this.selectSponsor = false;
    let orgId = '';
    if (this.sponsorSelection.id == 1) {
      orgId = '01';
    } else {
      orgId = this.sponsorSelection.id.toString();
    }
    this.userService
      .getUser(`sponsor${orgId}_%sponsor`)
      .subscribe((testUser) => {
        this.dbUser = testUser;
        this.isSponsor = true;
        this.isAdmin = false;
        this.canSeeSwitchToOriginal = true;
        this.canSeeSwitchToDriver = false;
        this.canSeeSwitchToSponsor = false;
      });
  }

  switchToOriginal() {
    this.dbUser = this.ogUser;
    if (this.ogUser.userType.toLowerCase() == 'admin') {
      this.isDriver = false;
      this.isSponsor = false;
      this.isAdmin = true;
      this.canSeeSwitchToDriver = true;
      this.canSeeSwitchToSponsor = true;
      this.canSeeSwitchToOriginal = false;
    } else {
      this.isDriver = false;
      this.isSponsor = true;
      this.canSeeSwitchToDriver = true;
      this.canSeeSwitchToOriginal = false;
    }
  }
}
