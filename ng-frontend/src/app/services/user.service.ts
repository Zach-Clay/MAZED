import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject, BehaviorSubject, Observable } from "rxjs";
import { catchError, retry } from 'rxjs/operators';
import { api_url, environment } from 'src/environments/environment';
import { user } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  //Get user with username = username
  public getUser(username: string) {
    return this.http.get<any>(`${api_url}/user/${username}`);
  }

  //Add user to the DB with post
  public registerUser(user: any) {
    return this.http.post<any>(`${api_url}/user`, user);
  }
  
}
