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
  selector: 'app-admin-homepage',
  templateUrl: './admin-homepage.component.html',
  styleUrls: ['./admin-homepage.component.css'],
})
export class AdminHomepageComponent implements OnInit {
  @Input() cognitoUser: any;
  @Input() dbUser!: User;
  constructor(
    private router: Router,
    private cognitoService: CognitoService,
    private userService: UserService,
    private pointsChangesService: PointsChangesService,
    private productListService: ProductListService
  ) {}

  ngOnInit(): void {
    console.log(this.dbUser);
  }
}
