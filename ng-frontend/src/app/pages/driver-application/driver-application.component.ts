import { Component, OnInit } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { UserService } from 'src/app/services/user.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { User, SponsorOrg } from 'src/app/models/interfaces';

@Component({
  selector: 'app-driver-application',
  templateUrl: './driver-application.component.html',
  styleUrls: ['./driver-application.component.css']
})
export class DriverApplicationComponent implements OnInit {
  submitted: boolean = false;
  user!: User;
  orgs!: SponsorOrg[];
  orgSelection!: SponsorOrg;
  description: string = "";

  constructor(
    private cognitoService: CognitoService,
    private userService: UserService,
    private sponsorOrgService: SponsorOrgService,
  ) { }

  ngOnInit(): void {
    //get the user
    this.cognitoService
      .getUser()
      .then((user: any) => {
        this.userService.getUser(user.username).subscribe((data)=>{
          this.user = data;
        })
      })
      .catch((err) => {
        console.log(err);
      });
    
    //get the sponsor orgs
    this.sponsorOrgService.getAllOrgs().subscribe((data) => {
      this.orgs = data;
    });
  }

  onSubmit() {
    if (!this.orgSelection) {
      alert("You must select an org to apply to");
      return;
    }
    if (this.description === "") {
      alert("You must provide a reason for you application");
      return;
    }

    //submit the application
    

  }

}
