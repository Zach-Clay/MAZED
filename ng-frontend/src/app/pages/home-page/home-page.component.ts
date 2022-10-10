import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { User, PointsChanges } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';
import { PointsChangesService } from 'src/app/services/points-changes.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  isAuthenticated: boolean;
  currentUser: any;
  dbUser!: User;
  displayName: boolean = false;
  pointTransactions!: PointsChanges[];

  constructor(
    private router: Router,
    private cognitoService: CognitoService,
    private userService: UserService,
    private pointsChangesService: PointsChangesService
  ) {
    this.isAuthenticated = false;
  }

  ngOnInit(): void {
    this.cognitoService.isAuthenticated().then((success: boolean) => {
      this.isAuthenticated = success;
      if (!this.isAuthenticated) {
        this.router.navigate(['/']);
      } else {
        this.currentUser = this.cognitoService
          .getUser()
          .then((user: any) => {
            this.currentUser = user;
            this.userService
              .getUser(this.currentUser.username)
              .subscribe((data) => {
                this.dbUser = data;
                this.pointsChangesService
                  .getTransactions(this.dbUser.id)
                  .subscribe((pointsTrans) => {
                    this.pointTransactions = pointsTrans;
                    // display points now
                    console.log(this.pointTransactions);
                  });
              });
          })
          .catch((e) => {
            console.log(e);
          });
      }
    });
  }

  signOut() {
    this.cognitoService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }
}
