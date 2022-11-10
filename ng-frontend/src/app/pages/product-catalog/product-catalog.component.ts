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
  selector: 'app-product-catalog',
  templateUrl: './product-catalog.component.html',
  styleUrls: ['./product-catalog.component.css']
})
export class ProductCatalogComponent implements OnInit {
  cognitoUser: any;
  dbUser!: User;
  isSponsor: boolean = false;
  isDriver: boolean = false;
  isAdmin: boolean = false;
  orgSelection!: SponsorOrg;
  loading: boolean = true;

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

}
