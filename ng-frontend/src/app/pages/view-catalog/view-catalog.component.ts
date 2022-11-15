import { Component, OnInit, Input, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService } from 'src/app/services/cognito.service';
import { User, SponsorOrg, Cart } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { ProductListService } from 'src/app/services/product-list.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { ItunesApiService } from 'src/app/services/itunes-api.service';
import { CartService } from 'src/app/services/cart.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface CheckoutDialogData {
  user: User;
  sponsor: SponsorOrg;
}

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
    private router: Router,
    private cartService: CartService,
    public dialog: MatDialog,
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

  onAddToCart(item: any) {
    const cartItem: Cart = {
      id: 0,
      userId: this.dbUser.id,
      sponsorId: this.orgSelection.id,
      productId: item.trackId
    }
    this.cartService.addToCart(cartItem)
    .subscribe((res) => {
    })
  }

  onCheckout() {
    const dialogRef = this.dialog.open(CheckoutDialog, {
      width: '80vw',
      data: { user: this.dbUser, sponsor: this.orgSelection },
    });
  }
}

@Component({
  selector: 'checkout-dialog',
  templateUrl: './checkout-dialog.html',
  styleUrls: ['./checkout-dialog.css'],
})
export class CheckoutDialog implements OnInit {
  cartItems!: Cart[];
  cartItemsLoading: boolean = true;
  currentProducts: any = [];
  userPoints: number = 0;
  cartTotalPoints: number = 0;

  constructor(
    public dialogRef: MatDialogRef<CheckoutDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CheckoutDialogData,
    private userService: UserService,
    private cartService: CartService,
    private iTunesService: ItunesApiService,
  ) {}

  ngOnInit(): void {
    //Get the cart items
    this.cartService.getCartByUserId(this.data.user.id)
    .subscribe((items) => {
      this.cartItems = items;

      //Get the products
      for (let product of this.cartItems) {
        this.iTunesService
          .getProducts(product.productId.toString(), 'all')
          .subscribe((data) => {
            //add point value
            const price = data.results[0].trackPrice;
            const conversion = this.data.sponsor.dollarToPoint;
            data.results[0].pointValue = price / conversion;
            // if the trackID is a multiple of 9, make the product unavailable to simulate unavailability
            data.results[0].available = data.results[0].trackId % 9 != 0;

            //update cart total points
            this.cartTotalPoints += data.results[0].pointValue;

            this.currentProducts.push(data.results[0]);
            if (this.currentProducts.length === this.cartItems.length) {
              this.cartItemsLoading = false;
            }
          });
      }
    })

    //Get the users points
    this.userService.getUserPointsBySponsor(this.data.user.id, this.data.sponsor.id)
    .subscribe((data) => {
      this.userPoints = data.userPoints;
    })
  }

  onDoneClick(): void {
    this.dialogRef.close();
  }

  deleteCartItem(trackId: number) {
    //delete from cart table
    this.cartService.getCartItemByUserSponsorTrackId(trackId, this.data.user.id, this.data.sponsor.id)
    .subscribe((cartItem) => {
      console.log(cartItem);

      this.cartService.deleteFromCart(cartItem.id)
      .subscribe(() => {

        this.cartItems = this.cartItems.filter(c => c.id !== cartItem.id);

        this.cartTotalPoints = 0;
        for (let i = 0; i < this.currentProducts.length; i++) {
          if (this.currentProducts[i].trackId === trackId) {
            this.currentProducts.splice(i, 1);
            continue;
          }
          this.cartTotalPoints += this.currentProducts[i].pointValue;
        }
      })
    })
  }

  onCheckout() {
    console.log(this.cartItems);

    //Add items to the order table


    //Remove items from cart table

  }

}
