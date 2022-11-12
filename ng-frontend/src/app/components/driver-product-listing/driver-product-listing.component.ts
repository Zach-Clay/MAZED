import { Component, OnInit, Input } from '@angular/core';
import {
  SponsorOrg,
  PointsChanges,
  User,
  Product,
} from '../../models/interfaces';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { ProductListService } from 'src/app/services/product-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-driver-product-listing',
  templateUrl: './driver-product-listing.component.html',
  styleUrls: ['./driver-product-listing.component.css'],
})
export class DriverProductListingComponent implements OnInit {
  @Input() currentSponsor!: SponsorOrg;
  @Input() dbUser!: User;
  @Input() clickable!: boolean;
  products!: Product[];
  fetchedProducts: boolean = false;

  constructor(
    private pointsChangesService: PointsChangesService,
    private productListService: ProductListService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productListService
      .getProductsBySponsorId(this.currentSponsor.id)
      .subscribe((data) => {
        this.products = data;
        this.fetchedProducts = true;
      });
  }

  goToCatalogue() {
    if (this.dbUser.userType.toLowerCase() != 'driver' || !this.clickable) {
      return;
    }
    this.router.navigate(['/view-catalog']);
  }
}
