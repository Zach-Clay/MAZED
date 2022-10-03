import { Component, OnInit } from '@angular/core';
import { UserInfo, LoginService } from 'src/app/services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loading: boolean;
  forgot: boolean;
  user: UserInfo;
  resetCode: string;
  newPassword: string;
  newPasswordRepeat: string;

  constructor(private router: Router, private loginService: LoginService) {
    this.loading = false;
    this.user = {} as UserInfo;
    this.forgot = false;
    this.resetCode = '';
    this.newPassword = '';
    this.newPasswordRepeat = '';
  }

  ngOnInit(): void {
    this.loginService.isAuthenticated().then((success: boolean) => {
      if (success) {
        this.router.navigate(['/home']);
      }
    });
  }

  forgotPassword() {
    if (!this.user.username) {
      alert('Please enter a username');
      return;
    }
    this.forgot = true;

    this.loginService
      .forgotPassword(this.user)
      .then(() => {})
      .catch(() => {});
  }

  submitForgotPassword() {
    if (!this.resetCode) {
      alert('Please enter the code you received in your email');
    }
    if (!this.newPassword || !this.newPasswordRepeat) {
      alert('Please enter in password');
    }
    if (this.newPassword != this.newPasswordRepeat) {
      alert('Passwords do not match');
    }
    this.user.code = this.resetCode;
    this.loginService
      .forgotPasswordSubmit(this.user, this.newPassword)
      .then((success) => {
        // call our own API and log the password change
        this.resetCode = '';
        this.user.code = '';
        this.forgot = false;
      })
      .catch((error) => {
        alert(error);
      });
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

  public signIn(): void {
    console.log('SIGN IN');
    console.log(this.user);
    this.loading = true;
    this.loginService
      .signIn(this.user)
      .then(() => {
        this.router.navigate(['/home']);
      })
      .catch((err) => {
        alert(err);
        this.loading = false;
      });
  }
}
