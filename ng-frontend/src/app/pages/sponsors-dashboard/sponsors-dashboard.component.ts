import { Component, Input, OnInit, Inject } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import {
  User,
  SponsorOrg,
  PointsChanges,
  UserToSponsor,
} from 'src/app/models/interfaces';
import { ViewChild, ElementRef } from '@angular/core';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { ElementSchemaRegistry } from '@angular/compiler';

export interface DialogData {
  Drivers: User[];
  OrgSelection: SponsorOrg;
}

@Component({
  selector: 'app-sponsors-dashboard',
  templateUrl: './sponsors-dashboard.component.html',
  styleUrls: ['./sponsors-dashboard.component.css'],
})
export class SponsorsDashboardComponent implements OnInit {
  user!: User;
  ogUser!: User;
  orgs!: SponsorOrg[];
  orgSelection!: SponsorOrg;
  editing: boolean = false;
  showData: boolean = false;
  drivers!: User[];
  sponsorsOrg!: SponsorOrg;
  addingSponsor: boolean = false;
  canSeeSwitchToDriver: boolean = false;
  canSeeSwitchToSponsor: boolean = false;
  canSeeSwitchToOriginal: boolean = false;
  selectDriver: boolean = false;
  selectSponsor: boolean = false;
  sponsorSelection!: SponsorOrg;

  @ViewChild('orgDesc') orgDescription!: ElementRef;
  @ViewChild('dollarToPoint') dollarToPoint!: ElementRef;

  constructor(
    private cognitoService: CognitoService,
    private router: Router,
    private userService: UserService,
    private sponsorOrgService: SponsorOrgService,
    public dialog: MatDialog
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
            this.ogUser.userType.toLowerCase() == 'sponsor' ||
            this.ogUser.userType.toLowerCase() == 'admin';
          this.canSeeSwitchToSponsor =
            this.ogUser.userType.toLowerCase() == 'admin';

          //Get the orgs
          this.sponsorOrgService.getAllOrgs().subscribe((data) => {
            this.orgs = data;
            this.showData = true;
          });

          //If the user is a sponsor -- get their sponsorOrg
          if (this.user.userType.toLowerCase() === 'sponsor') {
            this.userService
              .getSponsorOrgBySponsorUserId(this.user.id)
              .subscribe((org) => {
                this.sponsorsOrg = org;
              });
          }
        });
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onSelectionChange() {
    this.drivers = [];
    //update driver list
    this.userService
      .getDriverUsersBySponsorOrgId(this.orgSelection.id)
      .subscribe((data) => {
        data.forEach((driver) => {
          if (!driver.username.includes('%driver')) {
            this.drivers.push(driver);
          }
        });
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

  navToEditCatalog() {
    this.router.navigate(['/edit-catalog']);
  }

  removeDriver(driver: User) {
    if (
      confirm(
        'Are you sure you want to remove this driver? This action cannot be undone...'
      )
    ) {
      //Remove the driver from the sponsor
      this.userService
        .getUserToSponsorEntriesByDriverUsersId(driver.id)
        .subscribe((data) => {
          let entries: UserToSponsor[] = data;
          entries = entries.filter((e) => e.sponsorId === this.orgSelection.id);
          this.userService.removeUserFromSponsorOrg(entries[0].id);
          this.drivers = this.drivers.filter((d) => d.id !== driver.id);
        });
    }
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddDeductDialog, {
      width: '1000px',
      data: { Drivers: this.drivers, OrgSelection: this.orgSelection },
    });
  }

  signOut() {
    this.cognitoService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }

  switchToDriver(showSelection: boolean) {
    if (showSelection && this.ogUser.userType.toLowerCase() == 'admin') {
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
    if (showSponsorSelection && this.ogUser.userType.toLowerCase() == 'admin') {
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
        this.sponsorsOrg = this.sponsorSelection;
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

@Component({
  selector: 'add-deduct-dialog',
  templateUrl: './add-deduct-dialog.html',
  //styleUrls: ['./add-deduct-dialog.css']
})
export class AddDeductDialog implements OnInit {
  driverSelection!: User;
  pointAmount!: number;
  reason!: string;
  driverPoints!: number;

  constructor(
    public dialogRef: MatDialogRef<AddDeductDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private userService: UserService,
    private pointChangesService: PointsChangesService
  ) {}

  ngOnInit(): void {}

  selectionChange(driver: User) {
    this.userService
      .getUserPointsBySponsor(driver.id, this.data.OrgSelection.id)
      .subscribe((data) => {
        this.driverPoints = data.userPoints;
      });
  }

  onDoneClick(): void {
    this.dialogRef.close();
  }

  updatePoints() {
    if (confirm('Are you sure you want to add/deduct points?')) {
      if (this.pointAmount === 0) {
        return;
      }
      //TODO
      let pointValue = this.pointAmount;

      const pointTrans: PointsChanges = {
        pointId: 0,
        sponsorId: this.data.OrgSelection.id,
        userId: this.driverSelection.id,
        pointValue: pointValue,
        reason: this.reason,
        modDate: Date.now(),
      };

      //update the point transaction
      this.pointChangesService.postTransation(pointTrans);

      //to update the user real time
      this.driverPoints += pointValue;

      this.pointAmount = 0;
      this.reason = '';
    }
  }
}
