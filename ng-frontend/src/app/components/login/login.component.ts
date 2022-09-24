import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor() {}
  username!: string;
  password!: string;

  ngOnInit(): void {}

  forgotUsername() {
    alert('Forgot username');
  }
  forgotPassword() {
    alert('Forgot password');
  }

  onSubmit() {
    if (!this.username) {
      alert('Please enter your username!');
      return;
    }
    if (!this.password) {
      alert('Please enter your password!');
      return;
    }
    console.log(this.username, this.password);
    this.username = '';
    this.password = '';
  }
}
