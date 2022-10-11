import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { LoginAttempt } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class LoginAttemptService {

  constructor(private http: HttpClient) { }

  //Get all attemps with username == username
  public getAttempts(username: string) {
    return this.http.get<LoginAttempt[]>(`${api_url}/LoginAttempt/${username}`);
  }

  //Post a new login attempt
  public addAttempt(attempt: LoginAttempt) {
    this.http.post<LoginAttempt[]>(`${api_url}/LoginAttempt`, attempt).subscribe({
      next: data => {
        return data;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }
}
