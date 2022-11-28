import { Component, OnInit } from '@angular/core';
import { CognitoService, UserInfo } from 'src/app/services/cognito.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/interfaces';
import { format } from 'path';
import { runInThisContext } from 'vm';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css'],
})
export class ProfilePageComponent implements OnInit {
  user!: User;
  ogUser!: User;
  selectedUser!: User;
  userList!: User[];
  isDisabled: boolean = true;
  infoLoaded: boolean = false;
  selectLoaded: boolean = false;
  prettyPhoneNum: string = '';
  isDriver: boolean = false;
  isSponsor: boolean = false;
  isAdmin: boolean = false;
  changingPassword: boolean = false;
  resetCode: string = '';
  newPassword: string = '';
  newPasswordRepeat: string = '';
  canSeeSwitchToDriver = false;
  canSeeSwitchToSponsor = false;

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
    private cognitoService: CognitoService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userList = [];
    this.cognitoService
      .getUser()
      .then((user: any) => {
        this.userService.getUser(user.username).subscribe((data) => {
          this.user = data;
          this.ogUser = data;
          this.selectedUser = this.user;
          this.infoLoaded = true;

          this.canSeeSwitchToDriver =
            this.ogUser.userType.toLowerCase() == 'sponsor';

          //Get info for multiselect box
          if (this.user.userType.toLowerCase() === 'driver')
            this.isDriver = true;
          //If they are a sponsor - they need to be able to select all of their driver profiles
          if (this.user.userType.toLowerCase() === 'sponsor') {
            this.isSponsor = true;

            //Get the sponsors sponsor org
            this.userService
              .getSponsorOrgBySponsorUserId(this.user.id)
              .subscribe((org) => {
                //Then get the drivers for that org
                this.userService
                  .getDriverUsersBySponsorOrgId(org.id)
                  .subscribe((drivers) => {
                    drivers.forEach((driver) => {
                      if (!driver.username.includes('%driver')) {
                        this.userList.push(driver);
                      }
                    });
                    this.selectLoaded = true;
                  });
              });
          }
          //If they are an admin, they can select an profile
          if (this.user.userType.toLowerCase() === 'admin') {
            this.isAdmin = true;
            //Get all users
            this.userService.getAllUsers().subscribe((users) => {
              this.userList = users;
              this.selectLoaded = true;
            });
          }
          this.formatPhone();
        });
      })
      .catch((err) => {
        console.log(err);
      });
  }

  editBtn() {
    if (this.canSeeSwitchToSponsor)
      alert('Cannot edit profile while in Driver View');
    else this.isDisabled = !this.isDisabled;
  }

  changePasswordBtn() {
    this.changingPassword = true;
    this.cognitoService
      .forgotPassword(this.user.username)
      .then((s) => {
        console.log(s);
      })
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
    this.cognitoService
      .forgotPasswordSubmit(
        this.user.username,
        this.resetCode,
        this.newPassword
      )
      .then((success) => {
        // call our own API and log the password change
        this.resetCode = '';
        this.newPassword = '';
        this.newPasswordRepeat = '';
        this.changingPassword = false;
      })
      .catch((error) => {
        alert(error);
      });
  }

  updateProfile() {
    if (confirm('Are you sure you want to update your information?')) {
      //re-format the phonenumber
      let temp = this.prettyPhoneNum;
      temp = temp.split(' ').join('');
      temp = temp.split('-').join('');
      temp = temp.split('(').join('');
      temp = temp.split(')').join('');
      this.selectedUser.userPhoneNum = temp;

      //update our database
      this.userService.updateUser(this.selectedUser.id, this.selectedUser);

      //Update cognito
      const fullName =
        this.selectedUser.userFname + ' ' + this.selectedUser.userLname;
      const cognitoAttributes = {
        address: this.selectedUser.userAddress,
        email: this.selectedUser.userEmail,
        name: fullName,
        phone_number: this.selectedUser.userPhoneNum,
      };
      this.cognitoService.updateUser(
        this.selectedUser.username,
        cognitoAttributes
      );

      //re-format the phone number to look pretty
      this.formatPhone();
    }
  }

  onSelectionChange() {
    this.formatPhone();
  }

  formatPhone() {
    let temp = this.selectedUser.userPhoneNum;
    var cleaned = ('' + temp).replace(/\D/g, '');
    var match = cleaned.match(/^(1|)?(\d{3})(\d{3})(\d{4})$/);
    if (match) {
      var intlCode = match[1] ? '+1 ' : '';
      this.prettyPhoneNum = [
        intlCode,
        '(',
        match[2],
        ') ',
        match[3],
        '-',
        match[4],
      ].join('');
    }
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

  signOut() {
    this.cognitoService.signOut().then(() => {
      this.router.navigate(['/']);
    });
  }

  switchToDriver() {
    let sponsorOrg = null;
    this.userService
      .getSponsorOrgBySponsorUserId(this.ogUser.id)
      .subscribe((org) => {
        sponsorOrg = org;
        this.userService
          .getUser(`sponsor${org.id}_%driver`)
          .subscribe((testUser) => {
            this.user = testUser;
            this.selectedUser = testUser;
            this.isSponsor = false;
            this.isDriver = true;
            this.canSeeSwitchToSponsor = true;
            this.canSeeSwitchToDriver = false;
          });
      });
  }

  switchToSponsor() {
    this.user = this.ogUser;
    this.selectedUser = this.ogUser;
    this.isSponsor = true;
    this.isDriver = false;
    this.canSeeSwitchToDriver = true;
    this.canSeeSwitchToSponsor = false;
  }
}
