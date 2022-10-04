import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {

  public FName: string = "Zach";
  public user: any;
  public isDisabled: boolean = true;

  constructor(private loginService: LoginService, private userService: UserService) { }

  ngOnInit(): void {
    this.loginService
      .getUser()
      .then((user) => {
        console.log(user);
        //this.userService.getUser(user.username).subscribe((data)=>{
        //   this.user = data;
        // })
      })
      .catch((err) => {
        console.log(err);
      });
  }

  editBtn() {
    this.isDisabled = !this.isDisabled;
  }

  updateProfile() {
    console.log("updating");
  }

}
