import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import {
  signUpForm,
  CognitoService,
  UserInfo,
} from '../../services/cognito.service';
import {
  CognitoUserPool,
  CognitoUserAttribute,
} from 'amazon-cognito-identity-js';
import { environment } from 'src/environments/environment';
import { runInThisContext } from 'vm';
import { UserService } from 'src/app/services/user.service';
import { Observable } from 'rxjs';
import { User, UserToSponsor } from 'src/app/models/interfaces';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css'],
})
export class UserRegistrationComponent implements OnInit {
  @Input() userType: string = 'driver';
  @Input() sponsorId: number = 0;
  public loading: boolean = false;
  public needsConfirmation: boolean = false;
  public name: any = '';
  public username: any = '';
  public email: any = '';
  public password: any = '';
  public repeat_password: any = '';
  public gender: any = '';
  public phone: any = '+1';
  public address: any = '';
  public date: any = '';
  public confirmationCode: any = '';
  user: UserInfo;
  newUser: User = {} as User;

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
    private userService: UserService
  ) {
    this.user = {} as UserInfo;
  }

  ngOnInit(): void {}

  selectGender(selection: number) {
    switch (selection) {
      case 0:
        this.gender = 'male';
        break;
      case 1:
        this.gender = 'female';
        break;
      case 2:
        this.gender = 'other';
        break;
      default:
        this.gender = 'not_say';
    }
  }

  onSubmit = () => {
    this.formatPhone();

    if (this.strengthColor !== 'green') {
      alert(
        'Password must be at least 8 characters long, contain at least one uppercase letter, contain at least one lowercase letter, contain at least one digit, and contain at least one special character'
      );
      return;
    }

    if (
      !this.name ||
      !this.username ||
      !this.email ||
      !this.password ||
      !this.repeat_password ||
      !this.phone ||
      !this.address ||
      !this.gender ||
      !this.date
    ) {
      alert('Please complete form');
      return;
    }
    if (this.password != this.repeat_password) {
      alert('Passwords do not match');
      return;
    }
    this.loading = true;
    var poolData = {
      UserPoolId: environment.cognito.userPoolId, // Your user pool id here
      ClientId: environment.cognito.userPoolWebClientId, // Your client id here
    };
    var userPool = new CognitoUserPool(poolData);
    var attributeList = [];
    let signUpData: signUpForm = {
      name: this.name,
      email: this.email,
      gender: this.gender,
      birthdate: this.date,
      phone_number: this.phone,
      address: this.address,
    };

    for (let key in signUpData) {
      let attrData = {
        Name: key,
        Value: signUpData[key],
      };
      let attribute = new CognitoUserAttribute(attrData);
      attributeList.push(attribute);
    }

    this.user.username = this.username;
    this.user.password = this.password;

    userPool.signUp(
      this.username,
      this.password,
      attributeList,
      [],
      (err, result) => {
        this.loading = false;
        if (err) {
          alert(err.message || JSON.stringify(err));
          return;
        }
        this.needsConfirmation = true;
      }
    );
  };

  confirmSignup() {
    this.loading = true;
    this.user.code = this.confirmationCode;
    this.cognitoService
      .confirmSignUp(this.user)
      .then((success) => {
        this.router.navigate(['/']);
      })
      .catch((error) => {
        this.loading = false;
        console.log(error);
      });

    //get users first and last name
    const nameArr = this.name.split(' ');
    let FName = nameArr[0];
    let LName = '';
    if (nameArr.length > 1) LName = nameArr[1];

    this.newUser.id = 0;
    this.newUser.username = this.username;
    this.newUser.userFname = FName;
    this.newUser.userLname = LName;
    this.newUser.userType = this.userType;
    this.newUser.userAddress = this.address;
    this.newUser.userEmail = this.email;
    this.newUser.userPwd = 'null';
    this.newUser.userPhoneNum = this.phone;
    this.newUser.isBlacklisted = 0;

    //post to our api
    this.userService.registerUser(this.newUser).subscribe((user) => {
      if (this.userType == 'sponsor') {
        let userToSponsor: UserToSponsor = {
          id: 0,
          userId: user.id,
          sponsorId: this.sponsorId,
          userPoints: 0,
          userType: this.userType,
        };
        this.userService.postUserToSponsor(userToSponsor);
      }
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
    this.timeout = setTimeout(() => this.StrengthChecker(this.password), 500);
    if (this.password.length !== 0) {
      this.displayType != 'block';
    } else {
      this.displayType = 'none';
    }
  };

  //Removes everyhing but + and numbers
  formatPhone = () => {
    this.phone = this.phone.replace(/[^+\d]+/g, '');
  };

  // checks to make sure that the user verifies their account, before navigating off the page
  // theres probably a better way to do this, directly in the html. but oh well
  returnToLogin() {
    if (this.needsConfirmation && !this.confirmationCode) {
      alert('You must verify your account before proceeding');
      return;
    }
    this.router.navigate(['/']);
  }
}
