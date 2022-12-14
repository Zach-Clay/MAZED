import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Amplify, Auth } from 'aws-amplify';
import * as AWS from 'aws-sdk';
import { Observable, BehaviorSubject, bufferToggle } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

export interface UserInfo {
  email: string;
  username: string;
  password: string;
  showPassword: boolean;
  code: string;
}

export interface signUpForm {
  name: string;
  email: string;
  gender: string;
  birthdate: string;
  phone_number: string;
  [key: string]: string;
}

@Injectable({
  providedIn: 'root',
})
export class CognitoService {
  private authSubject: BehaviorSubject<any>;

  private SESConfig: any = {
    region: 'us-east-1',
  };
  private cognitoidentityserviceprovider: any =
    new AWS.CognitoIdentityServiceProvider(this.SESConfig);

  constructor() {
    Amplify.configure({
      Auth: environment.cognito,
    });
    this.authSubject = new BehaviorSubject<boolean>(false);
    // AWS.config.update(this.SESConfig);
  }

  public signUp(user: UserInfo, attrList: string[]): Promise<any> {
    return Auth.signUp({
      username: user.username,
      password: user.password,
      attributes: attrList,
    });
  }

  public resendSignupCode(user: UserInfo): Promise<any> {
    return Auth.resendSignUp(user.username);
  }

  public confirmSignUp(user: UserInfo): Promise<any> {
    console.log('inside cognito service');
    return Auth.confirmSignUp(user.username, user.code);
  }

  public signIn(user: UserInfo): Promise<any> {
    return Auth.signIn(user.username, user.password).then(() => {
      this.authSubject.next(true);
    });
  }

  public signOut(): Promise<any> {
    return Auth.signOut().then(() => {
      this.authSubject.next(false);
    });
  }

  public isAuthenticated(): Promise<boolean> {
    if (this.authSubject.value) {
      return Promise.resolve(true);
    } else {
      return this.getUser()
        .then((user: any) => {
          if (user) {
            return true;
          } else {
            return false;
          }
        })
        .catch((e) => {
          return false;
        });
    }
  }

  public getUser(): Promise<any> {
    return Auth.currentUserInfo();
  }

  public updateUser(username: string, user: any): Promise<any> {
    let params = {
      UserPoolId: environment.cognito.userPoolId,
      Username: username,
    };
    this.cognitoidentityserviceprovider.adminGetUser(
      params,
      function (err: any, data: any) {
        if (err) console.log(err, err.stack); // an error occurred
        else console.log(data); // successful response
      }
    );
    return Auth.currentUserPoolUser().then((cognitoUser: any) => {
      return Auth.updateUserAttributes(cognitoUser, user);
    });
  }

  public forgotPassword(username: string): Promise<any> {
    return Auth.forgotPassword(username);
  }

  public forgotPasswordSubmit(
    username: string,
    code: string,
    newPassword: string
  ): Promise<any> {
    return Auth.forgotPasswordSubmit(username, code, newPassword);
  }
}
