import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  User,
  SponsorOrg,
  PointsChanges,
  Order,
} from 'src/app/models/interfaces';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { OrderService } from 'src/app/services/order.service';
import { transcode } from 'buffer';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css'],
})
export class ReportsComponent implements OnInit {
  @Input() user!: User;
  orgToViewReports!: SponsorOrg;
  needToSelectSponsor!: boolean;
  orgs!: SponsorOrg[];
  pointChangesForUser!: PointsChanges[];
  pointChangesForSponsor: PointsChanges[] = [];
  orderHistoryForUser!: Order[];
  orderHistoryForSponsor: Order[] = [];
  driversInOrg!: User[];
  driverToViewReports!: User;
  viewingEntireOrgReports!: boolean;
  viewingPoints!: boolean;
  viewingOrders!: boolean;

  constructor(
    private sponsorOrgService: SponsorOrgService,
    private userService: UserService,
    private pointsChangesService: PointsChangesService,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.sponsorOrgService.getAllOrgs().subscribe((data) => {
      this.orgs = data;
    });
    if (this.user.userType.toLowerCase() == 'admin') {
      this.needToSelectSponsor = true;
    } else {
      this.userService
        .getSponsorOrgBySponsorUserId(this.user.id)
        .subscribe((s) => {
          this.orgToViewReports = s;
          this.userService
            .getDriverUsersBySponsorOrgId(s.id)
            .subscribe((drivers) => {
              this.driversInOrg = drivers;
            });
        });
    }
  }

  onReportsOrgSelectionChange() {
    this.pointChangesForUser = [];
    this.pointChangesForSponsor = [];
    this.orderHistoryForUser = [];
    this.orderHistoryForSponsor = [];
    this.viewingEntireOrgReports = false;
    this.viewingOrders = false;
    this.viewingPoints = false;
    this.userService
      .getDriverUsersBySponsorOrgId(this.orgToViewReports.id)
      .subscribe((drivers) => {
        this.driversInOrg = drivers;
      });
  }

  onReportsDriverSelectionChange() {
    this.viewingEntireOrgReports = false;
    this.pointsChangesService
      .getTransactions(this.driverToViewReports.id)
      .subscribe((transactions) => {
        this.pointChangesForUser = transactions;
      });
    this.orderService
      .getOrdersByDriverId(this.driverToViewReports.id)
      .subscribe((orders) => {
        this.orderHistoryForUser = orders;
      });
  }

  selectedEntireOrg() {
    this.viewingEntireOrgReports = true;
    this.pointChangesForSponsor = [];
    this.orderHistoryForSponsor = [];
    this.driversInOrg.forEach((driver) => {
      this.pointsChangesService
        .getTransactions(driver.id)
        .subscribe((transactions) => {
          if (transactions) {
            transactions.forEach((transaction) => {
              this.pointChangesForSponsor.push(transaction);
            });
          }
        });
      this.orderService.getOrdersByDriverId(driver.id).subscribe((orders) => {
        if (orders) {
          orders.forEach((order) => {
            this.orderHistoryForSponsor.push(order);
          });
        }
      });
    });
  }
}
