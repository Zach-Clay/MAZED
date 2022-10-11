import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { User } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  //Get user with username = username
  public getUser(username: string) {
    return this.http.get<User>(`${api_url}/user/${username}`);
  }

  public getUserById(id: number) {
    return this.http.get<User>(`${api_url}/user/id/${id}`);
  }

  //Add user to the DB with post
  public registerUser(user: User) {
    this.http.post<User>(`${api_url}/user`, user).subscribe({
      next: data => {
        return data;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }

  //Update user's information
  public updateUser(id: number, user: User) {
    this.http.put(`${api_url}/user/${id}`, user).subscribe({
      next: data => {
        return data;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }
  
}
