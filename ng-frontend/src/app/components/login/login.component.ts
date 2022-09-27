import { Component, OnInit } from '@angular/core';
import { UserInfo, LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  user: UserInfo;

  constructor() {
    this.user = {} as UserInfo;
  }

  ngOnInit(): void {}

  forgotUsername() {
    alert('Forgot username');
  }
  forgotPassword() {
    alert('Forgot password');
  }

  onSubmit() {
    if (!this.user.username) {
      alert('Please enter your username!');
      return;
    }
    if (!this.user.password) {
      alert('Please enter your password!');
      return;
    }
    console.log(this.user.username, this.user.password);
    this.user.username = '';
    this.user.password = '';
  }
}
