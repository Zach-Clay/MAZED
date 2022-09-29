import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { api_url, environment } from 'src/environments/environment';
import { User } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  //Get user with username = username
  getUser = (username: string) => {
    this.http.get<any>(`${api_url}/user/${username}`).subscribe({
        next: data => {
          console.log('SUCCESS:  ' + data);
          return data;
        },
        error: err => {
          console.error('ERROR: ' + err);
        }
    });
  }

  //Add user to the DB with post
  registerUser = (user: User) => {
    this.http.post<any>(`${api_url}/user`, user).subscribe({
        next: data => {
            console.log('SUCESS: ' + data);
            return data;
        },
        error: err => {
          console.error('ERROR: ' + err);
        }
    })
  }

}
