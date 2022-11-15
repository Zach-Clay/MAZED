import { Component, OnInit, Input } from '@angular/core';
import { Product, SponsorOrg } from 'src/app/models/interfaces';
import { ItunesApiService } from 'src/app/services/itunes-api.service';

@Component({
  selector: 'app-product-listing',
  templateUrl: './product-listing.component.html',
  styleUrls: ['./product-listing.component.css'],
})
export class ProductListingComponent implements OnInit {
  @Input() product!: Product;
  @Input() sponsor!: SponsorOrg;
  itunes_product: any;
  loaded: boolean = false;

  constructor(private itunesApiService: ItunesApiService) {}

  ngOnInit(): void {
    this.itunesApiService
      .getProducts(this.product.trackId.toString(), 'all')
      .subscribe((data) => {
        this.itunes_product = data.results[0];

        const price = data.results[0].trackPrice;
        const conversion = this.sponsor.dollarToPoint;
        data.results[0].pointValue = price / conversion;
        this.loaded = true;
      });
  }
}
