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
  user: UserInfo;

  constructor(private router: Router, private loginService: LoginService) {
    this.loading = false;
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

  public signIn(): void {
    console.log('SIGN IN');
    console.log(this.user);
    this.loading = true;
    this.loginService
      .signIn(this.user)
      .then(() => {
        this.router.navigate(['/register']);
      })
      .catch((err) => {
        alert(err);
        this.loading = false;
      });
  }
}
