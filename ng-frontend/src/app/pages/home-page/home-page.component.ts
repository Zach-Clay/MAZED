import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  isAuthenticated: boolean;
  currentUser: any;

  constructor(private router: Router, private cognitoService: CognitoService) {
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
            console.log(user);
            this.currentUser = user;
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
