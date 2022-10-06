import { Component, OnInit } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/interfaces';
import { format } from 'path';
import { runInThisContext } from 'vm';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {

  public user: User = {} as User;
  public isDisabled: boolean = true;
  public showInfo: boolean = false;
  public prettyPhoneNum: string = "";

  constructor(private cognitoService: CognitoService, private userService: UserService) { }

  ngOnInit(): void {
    this.cognitoService
      .getUser()
      .then((user: any) => {
        this.userService.getUser(user.username).subscribe((data)=>{
          this.user = data;
          this.showInfo = true;
          this.formatPhone();
        })
      })
      .catch((err) => {
        console.log(err);
      });
  }

  editBtn() {
    this.isDisabled = !this.isDisabled;
  }

  updateProfile() {
    //re-format the phonenumber
    let temp = this.prettyPhoneNum;
    temp = temp.split(' ').join('');
    temp = temp.split('-').join('');
    temp = temp.split('(').join('');
    temp = temp.split(')').join('');
    this.user.userPhoneNum = temp;

    //update our database
    this.userService.updateUser(this.user.id, this.user);

    //Update cognito
    const fullName = this.user.userFname + ' ' + this.user.userLname;
    const cognitoAttributes = {
      address: this.user.userAddress,
      email: this.user.userEmail,
      name: fullName,
      phone_number: this.user.userPhoneNum
    };
    this.cognitoService.updateUser(cognitoAttributes).then((temp) => {
      console.log(temp);
    });

    //re-format the phone number to look pretty
    this.formatPhone();
  }

  formatPhone() {
    let temp = this.user.userPhoneNum;
    var cleaned = ('' + temp).replace(/\D/g, '');
    var match = cleaned.match(/^(1|)?(\d{3})(\d{3})(\d{4})$/);
    if (match) {
      var intlCode = (match[1] ? '+1 ' : '');
      this.prettyPhoneNum =  [intlCode, '(', match[2], ') ', match[3], '-', match[4]].join('');
    }
  }

}
