import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService } from 'src/app/services/cognito.service';
import { User, SponsorOrg } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { ProductListService } from 'src/app/services/product-list.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { ItunesApiService } from 'src/app/services/itunes-api.service';

@Component({
  selector: 'app-view-catalog',
  templateUrl: './view-catalog.component.html',
  styleUrls: ['./view-catalog.component.css'],
})
export class ViewCatalogComponent implements OnInit {
  isAuthenticated: boolean = false;
  cognitoUser: any;
  dbUser!: User;
  isSponsor: boolean = false;
  isAdmin: boolean = false;
  isDriver: boolean = false;
  loading: boolean = true;

  orgChoices!: SponsorOrg[];
  orgSelection!: SponsorOrg;
  currentProducts: any = [];
  orgCatalogLoading: boolean = false;

  constructor(
    private cognitoService: CognitoService,
    private userService: UserService,
    private productListService: ProductListService,
    private sponsorOrgService: SponsorOrgService,
    private iTunesService: ItunesApiService,
    private router: Router
  ) {}

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
                this.dbUser = data;

                if (this.dbUser.userType.toLowerCase() === 'driver') {
                  this.isDriver = true;
                  //Get the drivers org(s) for selection
                  this.userService
                    .getSponsorOrgsByDriverUserId(this.dbUser.id)
                    .subscribe((orgs) => {
                      this.orgChoices = orgs;
                      this.loading = false;
                    });
                }

                if (this.dbUser.userType.toLowerCase() === 'sponsor') {
                  this.isSponsor = true;
                  //Get the sponsors org
                  this.userService
                    .getSponsorOrgBySponsorUserId(this.dbUser.id)
                    .subscribe((org) => {
                      this.orgSelection = org;
                      this.loading = false;
                      this.onSelectionChange();
                    });
                }

                if (this.dbUser.userType.toLowerCase() === 'admin') {
                  this.isAdmin = true;
                  //Get all orgs for selection
                  this.sponsorOrgService.getAllOrgs().subscribe((orgs) => {
                    this.orgChoices = orgs;
                    this.loading = false;
                  });
                }
              });
          })
          .catch((e) => {
            console.log(e);
          });
      }
    });
  }

  onSelectionChange() {
    this.orgCatalogLoading = true;
    this.currentProducts = [];

    //Get the products for the chosen org
    this.productListService
      .getProductsBySponsorId(this.orgSelection.id)
      .subscribe((products) => {
        if (products.length === 0) {
          this.orgCatalogLoading = false;
        }
        for (let product of products) {
          this.iTunesService
            .getProducts(product.trackId.toString(), 'all')
            .subscribe((data) => {
              //console.log(data);
              //add selected field
              data.results[0].selected = false;

              //add point value
              const price = data.results[0].trackPrice;
              const conversion = this.orgSelection.dollarToPoint;
              data.results[0].pointValue = price / conversion;
              // if the trackID is a multiple of 9, make the product unavailable to simulate unavailability
              data.results[0].available = data.results[0].trackId % 9 != 0;

              this.currentProducts.push(data.results[0]);
              if (this.currentProducts.length === products.length) {
                this.orgCatalogLoading = false;
              }
            });
        }
      });
  } //end onSelectionChange()

  navToEditCatalog() {
    this.router.navigate(['/edit-catalog']);
  }

  onAddToCart() {}

  onCheckout() {}
}
