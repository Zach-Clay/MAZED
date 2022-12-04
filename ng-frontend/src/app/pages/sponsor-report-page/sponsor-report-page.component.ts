import { Component, OnInit } from '@angular/core';
import { CognitoService } from 'src/app/services/cognito.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/interfaces';

@Component({
  selector: 'app-sponsor-report-page',
  templateUrl: './sponsor-report-page.component.html',
  styleUrls: ['./sponsor-report-page.component.css'],
})
export class SponsorReportPageComponent implements OnInit {
  user!: User;
  constructor(
    private cognitoService: CognitoService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.cognitoService.getUser().then((user: any) => {
      this.userService.getUser(user.username).subscribe((data) => {
        this.user = data;
      });
    });
  }
}
