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
  fetchedProducts!: boolean;
  p2drate!: string;
  currentSponsor!: SponsorOrg;
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
    this.fetchedProducts = false;
    this.pointsChangesService
      .getTransactions(this.dbUser.id)
      .subscribe((pointsTrans) => {
        this.pointTransactions = pointsTrans;
        
        // TODO 

        // this.sponsorOrgService
        //   .getSponsorOrg(this.dbUser.sponsorId)
        //   .subscribe((org) => {
        //     this.currentSponsor = org;
        //     this.p2drate = this.currentSponsor.dollarToPoint.toFixed(2);
        //     this.fetchedSponsor = true;
        //   });

        // display points now
        this.fetchedSponsor = true;
      });

      //TODO
    // this.productListService
    //   .getProductsBySponsorId(this.dbUser.sponsorId)
    //   .subscribe((data) => {
    //     this.products = data;
    //     this.fetchedProducts = true;
    //   });
    this.fetchedProducts = true;
  }
}
