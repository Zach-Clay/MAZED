import { Component, Input, OnInit, Inject } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { OrderService } from 'src/app/services/order.service';
import { User, Order, SponsorOrg } from 'src/app/models/interfaces';
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
  canSeeSwitchToDriver: boolean = false;
  canSeeSwitchToSponsor: boolean = false;
  canSeeSwitchToOriginal: boolean = false;
  selectDriver: boolean = false;
  selectSponsor: boolean = false;
  sponsorSelection!: SponsorOrg;
  allOrgs!: any;

  constructor(
    private cognitoService: CognitoService,
    private userService: UserService,
    private orderService: OrderService,
    private router: Router,
    private sponsorOrgService: SponsorOrgService
  ) {}

  ngOnInit(): void {
    this.cognitoService
      .getUser()
      .then((user: any) => {
        //Get the user
        this.userService.getUser(user.username).subscribe((data) => {
          this.user = data;
          this.ogUser = data;

          this.sponsorOrgService.getAllOrgs().subscribe((data) => {
            this.allOrgs = data;
          });
          this.canSeeSwitchToDriver =
            this.ogUser.userType.toLowerCase() == 'sponsor' ||
            this.ogUser.userType.toLowerCase() == 'admin';
          this.canSeeSwitchToSponsor =
            this.ogUser.userType.toLowerCase() == 'admin';

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

  switchToDriver(showSelection: boolean) {
    if (showSelection) {
      this.selectDriver = true;
      return;
    }
    if (this.ogUser.userType.toLowerCase() == 'admin') {
      this.selectDriver = false;
      console.log('hello');

      this.userService
        .getUser(`sponsor${this.sponsorSelection.id}_%driver`)
        .subscribe((testUser) => {
          this.user = testUser;
          this.canSeeSwitchToOriginal = true;
          this.canSeeSwitchToDriver = false;
          this.canSeeSwitchToSponsor = false;
        });
    } else {
      this.userService
        .getSponsorOrgBySponsorUserId(this.ogUser.id)
        .subscribe((org) => {
          this.userService
            .getUser(`sponsor${org.id}_%driver`)
            .subscribe((testUser) => {
              this.user = testUser;
              this.canSeeSwitchToOriginal = true;
              this.canSeeSwitchToDriver = false;
            });
        });
    }
  }

  switchToSponsor(showSponsorSelection: boolean) {
    if (showSponsorSelection) {
      this.selectSponsor = true;
      return;
    }

    this.selectSponsor = false;
    let orgId = '';
    if (this.sponsorSelection.id == 1) {
      orgId = '01';
    } else {
      orgId = this.sponsorSelection.id.toString();
    }
    this.userService
      .getUser(`sponsor${orgId}_%sponsor`)
      .subscribe((testUser) => {
        this.user = testUser;
        this.canSeeSwitchToOriginal = true;
        this.canSeeSwitchToDriver = false;
        this.canSeeSwitchToSponsor = false;
      });
  }

  switchToOriginal() {
    this.user = this.ogUser;
    if (this.ogUser.userType.toLowerCase() == 'admin') {
      this.canSeeSwitchToDriver = true;
      this.canSeeSwitchToSponsor = true;
      this.canSeeSwitchToOriginal = false;
    } else {
      this.canSeeSwitchToDriver = true;
      this.canSeeSwitchToOriginal = false;
    }
  }
}
