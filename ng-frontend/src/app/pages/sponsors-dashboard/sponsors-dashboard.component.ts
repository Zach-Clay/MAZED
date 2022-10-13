import { Component, Input, OnInit } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { User, SponsorOrg, Application } from 'src/app/models/interfaces';
import { ViewChild, ElementRef } from '@angular/core';
import { threadId } from 'worker_threads';

@Component({
  selector: 'app-sponsors-dashboard',
  templateUrl: './sponsors-dashboard.component.html',
  styleUrls: ['./sponsors-dashboard.component.css'],
})
export class SponsorsDashboardComponent implements OnInit {
  user!: User;
  orgs!: SponsorOrg[];
  orgSelection!: SponsorOrg;
  editing: boolean = false;
  showData: boolean = false;
  drivers!: User[];

  @ViewChild('orgDesc') orgDescription!: ElementRef;
  @ViewChild('dollarToPoint') dollarToPoint!: ElementRef;

  constructor(
    private cognitoService: CognitoService,
    private router: Router,
    private userService: UserService,
    private sponsorOrgService: SponsorOrgService
  ) {}

  ngOnInit(): void {
    this.cognitoService
      .getUser()
      .then((user: any) => {
        //Get the user
        this.userService.getUser(user.username).subscribe((data) => {
          this.user = data;

          //Get the orgs
          this.sponsorOrgService.getAllOrgs().subscribe((data) => {
            this.orgs = data;

            if (this.user.sponsorId !== 0) {
              this.sponsorOrgService
                .getSponsorOrg(this.user.sponsorId)
                .subscribe((o) => {
                  this.orgSelection = o;
                  //get drivers for that org
                  this.onSelectionChange();
                  this.showData = true;
                });
            } else {
              this.showData = true;
            }
          });
        });
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onSelectionChange() {
    //update driver list
    this.userService
      .getDriversBySponsor(this.orgSelection.id)
      .subscribe((data) => {
        this.drivers = data;
      });
  }

  toggleEdit() {
    this.editing = !this.editing;
  }

  updateOrg() {
    if (confirm('Are you sure you want to save? ')) {
      let orgDesc = this.orgDescription.nativeElement.innerText;

      let dTP = this.dollarToPoint.nativeElement.innerText;
      dTP = dTP.split('$').join('');

      this.orgSelection.orgDescription = orgDesc;
      this.orgSelection.dollarToPoint = dTP;

      //make api calls to update org info
      this.sponsorOrgService.updateSponsorOrg(
        this.orgSelection.id,
        this.orgSelection
      );
    }
  }

  removeDriver(driver: User) {
    if (
      confirm(
        'Are you sure you want to remove this driver? This action cannot be undone...'
      )
    ) {
      driver.sponsorId = 0;
      //make api calls to remove driver
      this.userService.updateUser(driver.id, driver);
      this.drivers = this.drivers.filter((d) => d.id !== driver.id);
    }
  }
}
