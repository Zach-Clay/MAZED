import { Component, OnInit } from '@angular/core';
import { UserInfo, CognitoService } from 'src/app/services/cognito.service';
import { Router } from '@angular/router';
import { LoginAttemptService } from 'src/app/services/login-attempt.service';
import { LoginAttempt } from 'src/app/models/interfaces';

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

  public timeout: any;
  public strongPassword = new RegExp(
    '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})'
  );
  public mediumPassword = new RegExp(
    '((?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{6,}))|((?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9])(?=.{8,}))'
  );
  public displayType: string = 'none';
  public strengthColor: string = 'red';
  public strengthText: string = 'Weak';

  constructor(
    private router: Router,
    private cognitoService: CognitoService,
    private loginAttemptService: LoginAttemptService
  ) {
    this.loading = false;
    this.user = {} as UserInfo;
    this.forgot = false;
    this.resetCode = '';
    this.newPassword = '';
    this.newPasswordRepeat = '';
  }

  ngOnInit(): void {
    this.cognitoService.isAuthenticated().then((success: boolean) => {
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

    this.cognitoService
      .forgotPassword(this.user)
      .then(() => {})
      .catch((e) => {
        console.log(e);
      });
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
    this.cognitoService
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
    this.loading = true;
    if (!this.user.username) {
      alert('Please enter your username!');
      this.loading = false;
      return;
    }
    if (!this.user.password) {
      alert('Please enter your password!');
      this.loading = false;
      return;
    }
    this.user.username = '';
    this.user.password = '';
    this.loading = false;
  }

  public signIn(): void {
    this.loading = true;
    if (!this.user.username) {
      alert('Please enter your username!');
      this.loading = false;
      return;
    }
    if (!this.user.password) {
      alert('Please enter your password!');
      this.loading = false;
      return;
    }
    event?.preventDefault();
    this.cognitoService
      .signIn(this.user)
      .then(() => {
        //log attempt to our db
        const attempt: LoginAttempt = {
          id: 0,
          username: this.user.username,
          isLoginSuccessful: 1,
        };
        this.loginAttemptService.addAttempt(attempt);
        this.loading = false;
        this.router.navigate(['/home']);
      })
      .catch((err) => {
        alert(err);
        this.loading = false;

        //log attempt to our DB
        const attempt: LoginAttempt = {
          id: 0,
          username: this.user.username,
          isLoginSuccessful: 0,
        };
        this.loginAttemptService.addAttempt(attempt);
      });
  }

  StrengthChecker = (PasswordParameter: any) => {
    if (this.strongPassword.test(PasswordParameter)) {
      this.strengthColor = 'green';
      this.strengthText = 'Strong';
    } else if (this.mediumPassword.test(PasswordParameter)) {
      this.strengthColor = 'blue';
      this.strengthText = 'Medium';
    } else {
      this.strengthColor = 'red';
      this.strengthText = 'Weak';
    }
  };

  eventListener = () => {
    this.displayType = 'block';
    clearTimeout(this.timeout);
    this.timeout = setTimeout(
      () => this.StrengthChecker(this.newPassword),
      500
    );
    if (this.newPassword.length !== 0) {
      this.displayType != 'block';
    } else {
      this.displayType = 'none';
    }
  };
}
