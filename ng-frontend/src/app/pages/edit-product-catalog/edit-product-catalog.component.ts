import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService } from 'src/app/services/cognito.service';
import { 
  User,
  SponsorOrg
} from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { ItunesApiService } from 'src/app/services/itunes-api.service';
import { asLiteral } from '@angular/compiler/src/render3/view/util';
import { ProductListService } from 'src/app/services/product-list.service';

@Component({
  selector: 'app-edit-product-catalog',
  templateUrl: './edit-product-catalog.component.html',
  styleUrls: ['./edit-product-catalog.component.css']
})
export class EditProductCatalogComponent implements OnInit {
  cognitoUser: any;
  dbUser!: User;
  isSponsor: boolean = false;
  isAdmin: boolean = false;
  orgSelection!: SponsorOrg;
  loading: boolean = true;
  radioSelection: string = "";

  loadingCurrentProducts: boolean = true;
  currentProducts: any = [];

  mediaTypes: string[] = ['Music', 'Movie', 'Podcast'];
  mediaSelection: string = "";
  searchItem: string = "";

  searchLoading: boolean = false;
  searchResults!: any;

  constructor(
    private cognitoService: CognitoService,
    private userService: UserService,
    private iTunesService: ItunesApiService,
    private productListService: ProductListService,
    ) { }

  ngOnInit(): void {
    this.cognitoUser = this.cognitoService.getUser().then((user: any) => {
      this.cognitoUser = user;
      this.userService.getUser(this.cognitoUser.username).subscribe((data) => {
        this.dbUser = data;

        //User should only be a sponsor or admin
        
        if (this.dbUser.userType.toLowerCase() === 'sponsor') {
          this.isSponsor = true;
          //Get the sponsors org
          this.userService.getSponsorOrgBySponsorUserId(this.dbUser.id)
            .subscribe((org) => {
              this.orgSelection = org;
              this.loading = false;
            })
        }
        if (this.dbUser.userType.toLowerCase() === 'admin') {
          this.isAdmin = true;
          //Allow the admin to select a sponsor org
          //TODO?
          this.loading = false;
        }

      });
    })
    .catch((e) => {
      console.log(e);
    });
  }

  refreshSearch() {
    if (this.mediaSelection === null || this.mediaSelection === "") {
      alert("You must select a media type");
      return;
    }
    if (this.searchItem === null || this.searchItem === "") {
      alert("You must input a search item");
      return;
    }

    this.searchLoading = true;

    //Get the products to display
    this.iTunesService.getProducts(this.searchItem, this.mediaSelection)
      .subscribe((products) => {
        this.searchResults = products.results;

        //Add a selected field to each product for bulk editing
        for (let i = 0; i < this.searchResults.length; i++) {
          this.searchResults[i].selected = false;

          //Add pointValue field to each product
          const price = this.searchResults[i].trackPrice;
          const conversion = this.orgSelection.dollarToPoint;
          this.searchResults[i].pointValue = price / conversion;
        }
        this.searchLoading = false;
      })

  }

  //on select, we need to change the selected field
  onSelect(item: any) {
    item.selected = !item.selected;
  }

  onSubmitUpdate() {
    //Get the items to add
    let itemsToAdd: number[] = [];
    for (let item of this.searchResults) {
      if (item.selected) itemsToAdd.push(item.trackId);
    }

    //If they have not selected an item
    if (itemsToAdd.length === 0) {
      alert("You must select an item(s) to add");
      return;
    }

    //Now submit the changes
    if (confirm("Are you sure you want to add these items to your catalog?")) {
      alert("Submitting...");
      this.productListService.postArrayOfTrackIds(this.orgSelection.id, itemsToAdd)
        .subscribe(() => {
          window.location.reload();
        })
    }
  }

  radioChange() {
    if (this.radioSelection === "remove") {
      if (this.currentProducts.length !== 0) {
        this.currentProducts.splice(0, this.currentProducts.length);
      }
      //Get all the current products for the organization
      this.productListService.getProductsBySponsorId(this.orgSelection.id)
      .subscribe((products) => {
        for (let product of products) {
          this.iTunesService.getProducts(product.trackId.toString(), "all")
          .subscribe((data) => {

            //add selected field
            data.results[0].selected = false;

            //add point value
            const price = data.results[0].trackPrice;
            const conversion = this.orgSelection.dollarToPoint;
            data.results[0].pointValue = price / conversion;
            
            this.currentProducts.push(data.results[0]);
            if (this.currentProducts.length === products.length) {
              this.loadingCurrentProducts = false;
            }
          })
        }
      })
    }
  }//end radioChange

  onRemoveItems() {
    //Get the items to add
    let itemsToRemove: number[] = [];
    for (let item of this.currentProducts) {
      if (item.selected) itemsToRemove.push(item.trackId);
    }

    //If they have not selected an item
    if (itemsToRemove.length === 0) {
      alert("You must select an item(s) to add");
      return;
    }

    //Now submit the changes
    if (confirm("Are you sure you want to remove these items from your catalog?")) {
      alert("Submitting...");
      this.productListService.deleteProductsByArrayOfTrackIds(this.orgSelection.id, itemsToRemove)
      .subscribe(() => {
        window.location.reload();
      });
    }
  }

}
