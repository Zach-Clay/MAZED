import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Order, User, Product } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { ItunesApiService } from 'src/app/services/itunes-api.service';

@Component({
  selector: 'app-order-report',
  templateUrl: './order-report.component.html',
  styleUrls: ['./order-report.component.css'],
})
export class OrderReportComponent implements OnInit {
  @Input() order!: Order;
  user!: User;
  prodList: any = [];
  items: any = [];
  constructor(
    private userService: UserService,
    private itunesService: ItunesApiService
  ) {}

  ngOnInit(): void {
    this.userService
      .getUserById(this.order.userId)
      .subscribe((u) => (this.user = u));
    this.prodList = JSON.parse(this.order.productList);
    this.prodList.forEach((product: any) => {
      this.itunesService
        .getProducts(product.ProductId.toString(), 'all')
        .subscribe((track) => {
          this.items.push(' '.concat(track.results[0].trackName));
        });
    });
  }
}
