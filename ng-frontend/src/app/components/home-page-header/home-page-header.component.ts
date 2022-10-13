import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';

@Component({
  selector: 'app-home-page-header',
  templateUrl: './home-page-header.component.html',
  styleUrls: ['./home-page-header.component.css'],
})
export class HomePageHeaderComponent implements OnInit {
  @Input() showPoints: boolean;
  image: string = '../assets/light-logo.png';
  constructor(private cognitoService: CognitoService, private router: Router) {
    this.showPoints = false;
  }

  ngOnInit(): void {}

  signOut() {
    this.cognitoService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }
}
