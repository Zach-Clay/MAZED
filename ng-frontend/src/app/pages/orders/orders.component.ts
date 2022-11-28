import { Component, Input, OnInit, Inject } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { OrderService } from 'src/app/services/order.service';
import { User, Order } from 'src/app/models/interfaces';
import { Users } from 'aws-sdk/clients/budgets';
import { CreateServiceSpecificCredentialResponse } from 'aws-sdk/clients/iam';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {
  user!: User;
  ogUser!: User;
  loading: boolean = true;
  orders!: Order[];
  canSeeSwitchToDriver = false;
  canSeeSwitchToSponsor = false;

  constructor(
    private cognitoService: CognitoService,
    private userService: UserService,
    private orderService: OrderService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.cognitoService
      .getUser()
      .then((user: any) => {
        //Get the user
        this.userService.getUser(user.username).subscribe((data) => {
          this.user = data;
          this.ogUser = data;

          this.canSeeSwitchToDriver =
            this.ogUser.userType.toLowerCase() == 'sponsor';

          //get user orders
          this.orderService
            .getOrdersByDriverId(this.user.id)
            .subscribe((res) => {
              this.orders = res;
              console.log(this.orders);
              this.loading = false;
            });
        });
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(order: Order) {
    if (confirm('Are you sure you want to cancel this order?')) {
      this.orderService.deleteOrder(order.id).subscribe(() => {
        this.orders = this.orders.filter((o) => o.id !== order.id);

        //Give points back to the driver
        this.userService.updateUserPointsBySponsorId(
          order.userId,
          order.sponsorId,
          order.totalPointVal
        );
      });
    }
  }

  signOut() {
    this.cognitoService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }

  switchToDriver() {
    let sponsorOrg = null;
    this.userService
      .getSponsorOrgBySponsorUserId(this.ogUser.id)
      .subscribe((org) => {
        sponsorOrg = org;
        this.userService
          .getUser(`sponsor${org.id}_%driver`)
          .subscribe((testUser) => {
            this.user = testUser;
            this.canSeeSwitchToSponsor = true;
            this.canSeeSwitchToDriver = false;
          });
      });
  }

  switchToSponsor() {
    this.user = this.ogUser;
    this.canSeeSwitchToDriver = true;
    this.canSeeSwitchToSponsor = false;
  }
}
