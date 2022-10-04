import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService, UserInfo } from 'src/app/services/login.service';

@Component({
  selector: 'app-home-page-header',
  templateUrl: './home-page-header.component.html',
  styleUrls: ['./home-page-header.component.css'],
})
export class HomePageHeaderComponent implements OnInit {
  image: string = '../assets/light-logo.png';
  constructor(private loginService: LoginService, private router: Router) {}

  ngOnInit(): void {}

  signOut() {
    this.loginService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }
}
