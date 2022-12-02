import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { User, UserToSponsor, PointsChanges } from '../../models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { ViewService } from 'src/app/services/view.service';

@Component({
  selector: 'app-home-page-header',
  templateUrl: './home-page-header.component.html',
  styleUrls: ['./home-page-header.component.css'],
})
export class HomePageHeaderComponent implements OnInit {
  @Input() showPoints: boolean;
  @Input() ogUser!: User;
  @Input() cognitoUser: any;
  dbUser!: User;
  totalPoints!: number;
  image: string = '../assets/light-logo.png';
  canSeeSwitchToDriver: boolean = false;
  constructor(
    private cognitoService: CognitoService,
    private router: Router,
    private userService: UserService,
    private pointChangesService: PointsChangesService,
    private sponsorOrgService: SponsorOrgService
  ) {
    this.showPoints = false;
    this.totalPoints = 0;
  }

  ngOnInit(): void {
    // this.canSeeSwitchToDriver = this.ogUser.userType.toLowerCase() == 'sponsor';
    // this.dbUser = this.ogUser;
  }

  signOut() {
    this.cognitoService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }

  getPointsPerSponsor() {}

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
          });
      });
  }
}
