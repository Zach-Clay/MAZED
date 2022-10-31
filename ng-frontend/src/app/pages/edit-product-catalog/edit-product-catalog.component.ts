import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService } from 'src/app/services/cognito.service';
import { 
  User,
  SponsorOrg
} from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { ItunesApiService } from 'src/app/services/itunes-api.service';

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

  mediaTypes: string[] = ['Music', 'Movie', 'Podcast'];
  mediaSelection: string = "";
  searchItem: string = "";

  searchLoading: boolean = false;
  searchResults!: any;

  constructor(
    private cognitoService: CognitoService,
    private userService: UserService,
    private iTunesService: ItunesApiService,
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
        }

        this.searchLoading = false;
      })

  }

}
