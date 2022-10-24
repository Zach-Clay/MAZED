import { Component, OnInit, Inject } from '@angular/core';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { User, SponsorOrg } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { ApplicationService } from 'src/app/services/application.service';
import { Application } from 'src/app/models/interfaces';
import { MatButtonToggleDefaultOptions } from '@angular/material/button-toggle';

export interface ExpandedApplication {
  id: number;
  userId: number;
  sponsorId: number;
  approvalStatus: number; //0 or 1
  applicantName: string;
  sponsorName: string;
  description: string;
  requestedDate: string;
  responseDate: string;
  decisionReason: string;
  isActive: number; //0 or 1
  expanded: boolean;
}

export interface SponsorDialogData {
  applications: ExpandedApplication[];
  user: User;
}

@Component({
  selector: 'app-sponsor-widget',
  templateUrl: './sponsor-widget.component.html',
  styleUrls: ['./sponsor-widget.component.css'],
})
export class SponsorWidgetComponent implements OnInit {
  user!: User;
  showSponsor: boolean = false;
  sponsorOrg!: SponsorOrg;
  isDriver: boolean = false;
  isSponsor: boolean = false;
  sponsorsApps!: Application[];
  showSponsorApps: boolean = false;
  driverOutstandingApps!: Application[];
  showDriverOustandingApps: boolean = false;
  loaded: boolean = false;

  constructor(
    private cognitoService: CognitoService,
    private userService: UserService,
    private sponsorOrgService: SponsorOrgService,
    private applicationService: ApplicationService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.cognitoService
      .getUser()
      .then((user: any) => {
        this.userService.getUser(user.username).subscribe((data) => {
          this.user = data;

          if (this.user.userType.toLowerCase() === 'driver') {
            this.isDriver = true;
          }
          if (this.user.userType.toLowerCase() === 'sponsor') {
            this.isSponsor = true;
          }

          

          //get sponsor info
          if (this.user.sponsorId !== 0) {
            this.sponsorOrgService
              .getSponsorOrg(this.user.sponsorId)
              .subscribe((org) => {
                this.sponsorOrg = org;
                this.showSponsor = true;
              });
          }

          if (this.isSponsor) {
            //get pending applications to sponsor
            this.applicationService
              .getApplicationsBySponsor(this.user.sponsorId)
              .subscribe((apps) => {
                this.sponsorsApps = apps;
              });
          }

          if (this.isDriver) {
            this.applicationService
              .getApplicationsByUser(this.user.id)
              .subscribe((apps) => {
                this.driverOutstandingApps = apps;
                this.driverOutstandingApps = this.driverOutstandingApps.filter(
                  (a) => a.isActive === 1
                );
                this.showDriverOustandingApps = true;
              });
          }
          this.loaded = true;
        });
      })
      .catch((err) => {
        console.log(err);
      });
  }

  openDialog(): void {
    let newSponsorApps: ExpandedApplication[] = [];
    //add expanded field to each sponsorApp
    for (let app of this.sponsorsApps) {
      let data: ExpandedApplication = {
        id: app.id,
        userId: app.userId,
        sponsorId: app.sponsorId,
        approvalStatus: app.approvalStatus,
        applicantName: app.applicantName,
        sponsorName: app.sponsorName,
        description: app.description,
        requestedDate: app.requestedDate,
        responseDate: app.responseDate,
        decisionReason: app.decisionReason,
        isActive: app.isActive,
        expanded: false,
      };

      if (data.isActive === 1) newSponsorApps.push(data);
    }

    const dialogRef = this.dialog.open(SponsorAppDialog, {
      width: '1000px',
      data: { applications: newSponsorApps, user: this.user },
    });

    // dialogRef.afterClosed().subscribe(result => {
    //   console.log('The dialog was closed');
    // });
  }
}

@Component({
  selector: 'sponsor-app-dialog',
  templateUrl: './sponsor-app-dialog.html',
  styleUrls: ['./sponsor-app-dialog.css'],
})
export class SponsorAppDialog implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<SponsorAppDialog>,
    @Inject(MAT_DIALOG_DATA) public data: SponsorDialogData,
    private applicationService: ApplicationService,
    private userService: UserService
  ) {}

  ngOnInit(): void {}

  toggleExpand(app: ExpandedApplication) {
    app.expanded = !app.expanded;
  }

  onDoneClick(): void {
    this.dialogRef.close();
  }

  updateApplication(app: ExpandedApplication, decision: string) {
    let approved = 0;
    if (decision === 'accept') approved = 1;

    const application: Application = {
      id: app.id,
      userId: app.userId,
      sponsorId: app.sponsorId,
      approvalStatus: approved,
      applicantName: app.applicantName,
      sponsorName: app.sponsorName,
      description: app.description,
      requestedDate: app.requestedDate,
      responseDate: app.responseDate,
      decisionReason: app.decisionReason,
      isActive: 0,
    };

    this.applicationService.updateApplication(application.id, application);

    //now update the driver's sponsor
    if (approved) {
      this.userService.getUserById(application.userId).subscribe((user) => {
        let userObj = user;
        userObj.sponsorId = application.sponsorId;
        this.userService.updateUser(userObj.id, userObj);
        alert(`driver ${decision}ed`);
        window.location.reload();
      });
    } else {
      alert(`driver ${decision}ed`);
      window.location.reload();
    }
  }
}
