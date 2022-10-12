import { Component, OnInit } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { User, SponsorOrg, Application } from 'src/app/models/interfaces';
import { ViewChild, ElementRef } from '@angular/core';

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

  @ViewChild('orgDesc') orgDescription!: ElementRef;
  @ViewChild('dollarToPoint') dollarToPoint!: ElementRef;

  constructor(
    private cognitoService: CognitoService,
    private router: Router,
    private userService: UserService,
    private sponsorOrgService: SponsorOrgService,
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

            // if (this.user.sponsorId !== 0) {
            //   this.orgSelection = this.orgs.find(o => o.id === this.user.sponsorId)
            // }
          });

        })
      })
      .catch((err) => {
        console.log(err);
    });
  }

  onSelectionChange() {
  }

  toggleEdit() {
    this.editing = !this.editing;
  }

  updateOrg() {
    let orgDesc = this.orgDescription.nativeElement.innerText

    let dTP = this.dollarToPoint.nativeElement.innerText;
    dTP = dTP.split("$").join("");
    console.log(dTP);
  }

}
