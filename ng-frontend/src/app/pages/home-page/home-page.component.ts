import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService, UserInfo } from 'src/app/services/login.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  isAuthenticated: boolean;
  currentUser: any;

  constructor(private router: Router, private loginService: LoginService) {
    this.isAuthenticated = false;
  }

  ngOnInit(): void {
    this.loginService.isAuthenticated().then((success: boolean) => {
      this.isAuthenticated = success;
      if (!this.isAuthenticated) {
        this.router.navigate(['/']);
      } else {
        this.currentUser = this.loginService
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
    this.loginService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }
}
