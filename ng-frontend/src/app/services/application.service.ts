import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { Application } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http: HttpClient) { }

  public getApplicationsBySponsor(sponsorId: number) {
    return this.http.get<Application[]>(`${api_url}/application/sponsor/${sponsorId}`);
  }

  public getApplicationsByUser(userId: number) {
    return this.http.get<Application[]>(`${api_url}/application/user/${userId}`);
  }

  public submitApplication(application: Application) {
    this.http.post<Application>(`${api_url}/application`, application).subscribe({
      next: data => {
        return data;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }

  public updateApplication(appId: number, application: Application) {
    this.http.put<Application>(`${api_url}/application/${appId}`, application).subscribe({
      next: data => {
        return data;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }

}
