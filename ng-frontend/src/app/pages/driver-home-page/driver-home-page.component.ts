import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import {
  User,
  PointsChanges,
  SponsorOrg,
  Product,
} from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { ProductListService } from 'src/app/services/product-list.service';
import { input } from '@aws-amplify/ui';

@Component({
  selector: 'app-driver-home-page',
  templateUrl: './driver-home-page.component.html',
  styleUrls: ['./driver-home-page.component.css'],
})
export class DriverHomePageComponent implements OnInit {
  @Input() cognitoUser: any;
  @Input() dbUser!: User;
  pointTransactions!: PointsChanges[];
  products!: Product[];
  p2drate!: string;
  currentSponsors!: SponsorOrg[];
  fetchedSponsor!: boolean;
  sponsorOrgs!: SponsorOrg[];

  constructor(
    private router: Router,
    private cognitoService: CognitoService,
    private userService: UserService,
    private pointsChangesService: PointsChangesService,
    private sponsorOrgService: SponsorOrgService,
    private productListService: ProductListService
  ) {}

  ngOnInit(): void {
    this.fetchedSponsor = false;
    this.userService
      .getSponsorOrgsByDriverUserId(this.dbUser.id)
      .subscribe((orgs) => {
        this.currentSponsors = orgs;
        this.fetchedSponsor = true;
      });
  }

  navToOrders() {
    this.router.navigate(['/orders']);
  }
}
