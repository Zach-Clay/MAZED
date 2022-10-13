import { Component, Input, OnInit, Inject } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { User, SponsorOrg, PointsChanges } from 'src/app/models/interfaces';
import { ViewChild, ElementRef } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { PointsChangesService } from 'src/app/services/points-changes.service';

export interface DialogData {
  Drivers: User[];
};


@Component({
  selector: 'app-sponsors-dashboard',
  templateUrl: './sponsors-dashboard.component.html',
  styleUrls: ['./sponsors-dashboard.component.css']
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
    private sponsorOrgService: SponsorOrgService,
    public dialog: MatDialog,
  ) { 
  }

  ngOnInit(): void {
    this.cognitoService
      .getUser()
      .then((user: any) => {
        //Get the user
        this.userService.getUser(user.username).subscribe((data)=>{
          this.user = data;

          //Get the orgs
          this.sponsorOrgService.getAllOrgs().subscribe((data) => {
            this.orgs = data;

            if (this.user.sponsorId !== 0) {
              this.sponsorOrgService.getSponsorOrg(this.user.sponsorId).subscribe((o) => {
                this.orgSelection = o;
                //get drivers for that org
                this.onSelectionChange();
                this.showData = true;
              })
              
            }
            else {
              this.showData = true;
            }
          });

        })
      })
      .catch((err) => {
        console.log(err);
    });
  }

  onSelectionChange() {
    //update driver list
    this.userService.getDriversBySponsor(this.orgSelection.id).subscribe((data) => {
      this.drivers = data;
    })
  }

  toggleEdit() {
    this.editing = !this.editing;
  }

  updateOrg() {
    if (confirm("Are you sure you want to save? ")) {
      let orgDesc = this.orgDescription.nativeElement.innerText

      let dTP = this.dollarToPoint.nativeElement.innerText;
      dTP = dTP.split("$").join("");

      this.orgSelection.orgDescription = orgDesc;
      this.orgSelection.dollarToPoint = dTP;

      //make api calls to update org info
      this.sponsorOrgService.updateSponsorOrg(this.orgSelection.id, this.orgSelection);
    }
  }

  removeDriver(driver: User) {
    if (confirm("Are you sure you want to remove this driver? This action cannot be undone...")) {
      driver.sponsorId = 0;
      //make api calls to remove driver
      this.userService.updateUser(driver.id, driver);
      this.drivers = this.drivers.filter(d => d.id !== driver.id);
    }
  }

  openDialog(): void {

    const dialogRef = this.dialog.open(AddDeductDialog, {
      width: '1000px',
      data: {Drivers: this.drivers},
    });
  }

}

@Component({
  selector: 'add-deduct-dialog',
  templateUrl: './add-deduct-dialog.html',
  //styleUrls: ['./add-deduct-dialog.css']
})
export class AddDeductDialog implements OnInit{

  driverSelection!: User;
  pointAmount!: number;
  reason!: string;

  constructor(
    public dialogRef: MatDialogRef<AddDeductDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private userService: UserService,
    private pointChangesService: PointsChangesService
  ) {}

  ngOnInit(): void {
  }

  onDoneClick(): void {
    this.dialogRef.close();
  }

  updatePoints() {
    if(confirm("Are you sure you want to add/deduct points?")) {
      if (this.pointAmount === 0) {
        return;
      }

      let pointValue = this.pointAmount;
      const pointTrans: PointsChanges = {
        pointId: 0,
        sponsorId: this.driverSelection.sponsorId,
        userId: this.driverSelection.id,
        pointValue: pointValue,
        reason: this.reason
      };

      //update the point transaction
      this.pointChangesService.postTransation(pointTrans);

      //to update the user real time
      this.driverSelection.totalPoints += pointValue;

      this.pointAmount = 0;
      this.reason = "";
    }
  }

}