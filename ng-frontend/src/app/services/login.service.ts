import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Amplify, Auth } from 'aws-amplify';
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
export class LoginService {
  private authSubject: BehaviorSubject<any>;

  constructor() {
    Amplify.configure({
      Auth: environment.cognito,
    });
    this.authSubject = new BehaviorSubject<boolean>(false);
  }

  public signUp(user: UserInfo, attrList: string[]): Promise<any> {
    return Auth.signUp({
      username: user.username,
      password: user.password,
      attributes: attrList,
    });
  }

  public confirmSignUp(user: UserInfo): Promise<any> {
    console.log('inside login service');
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
        .catch(() => {
          return false;
        });
    }
  }

  public getUser(): Promise<any> {
    return Auth.currentUserInfo();
  }

  public updateUser(user: UserInfo): Promise<any> {
    return Auth.currentUserPoolUser().then((cognitoUser: any) => {
      return Auth.updateUserAttributes(cognitoUser, user);
    });
  }
}
