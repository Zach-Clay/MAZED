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
