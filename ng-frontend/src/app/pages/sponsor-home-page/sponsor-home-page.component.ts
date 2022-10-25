import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import {
  User,
  PointsChanges,
  Product,
  SponsorOrg,
} from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { input } from '@aws-amplify/ui';
import { ProductListService } from 'src/app/services/product-list.service';

@Component({
  selector: 'app-sponsor-home-page',
  templateUrl: './sponsor-home-page.component.html',
  styleUrls: ['./sponsor-home-page.component.css'],
})
export class SponsorHomePageComponent implements OnInit {
  @Input() cognitoUser: any;
  @Input() dbUser!: User;
  sponsoredDrivers!: User[];
  gotSponsoredDrivers: boolean = false;
  currentProducts!: Product[];
  gotCurrentProducts: boolean = false;
  sponsorOrg!: SponsorOrg;

  constructor(
    private router: Router,
    private cognitoService: CognitoService,
    private userService: UserService,
    private pointsChangesService: PointsChangesService,
    private productListService: ProductListService
  ) {}

  ngOnInit(): void {
    //get the drivers for the sponsor
    this.userService
      .getSponsorOrgBySponsorUserId(this.dbUser.id)
      .subscribe((org) => {
        this.sponsorOrg = org;
        this.userService
          .getDriverUsersBySponsorOrgId(org.id)
          .subscribe((drivers) => {
            this.sponsoredDrivers = drivers;
            this.gotSponsoredDrivers = true;
          });
        //get products by sponsor id
        this.productListService
          .getProductsBySponsorId(this.sponsorOrg.id)
          .subscribe((data) => {
            this.currentProducts = data;
            this.gotCurrentProducts = true;
          });
      });
  }
}
