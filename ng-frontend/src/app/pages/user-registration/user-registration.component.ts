import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent implements OnInit {

  public name: any = '';
  public username: any = '';
  public email: any = '';
  public password: any = '';
  public repeat_password: any = '';
  public phone: any = '';
  public address: any = '';

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit = () => {
    console.log(this.name, this.username, this.email, this.password, this.repeat_password, this.phone, this.address);
  }

}
