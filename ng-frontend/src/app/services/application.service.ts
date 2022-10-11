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
    return this.http.get<Application[]>(`${api_url}/Application/sponsor/${sponsorId}`);
  }

  public getApplicationsByUser(userId: number) {
    return this.http.get<Application[]>(`${api_url}/Application/user/${userId}`);
  }

  public submitApplication(application: Application) {
    console.log("Submit this: ", application);
    const newApp = {
      id: 0,
      userId: application.userId,
      sponsorId: application.sponsorId,
      approvalStatus: application.approvalStatus,
      description: application.description,
      applicantName: application.applicantName,
      sponsorName: application.sponsorName,
      decisionReason: "",
      isActive: 1
    }
    this.http.post<Application>(`${api_url}/Application`, newApp).subscribe({
      next: data => {
        return data;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }

  public updateApplication(appId: number, application: Application) {
    console.log("Update to: ", application);
    this.http.put<Application>(`${api_url}/Application/${appId}`, application).subscribe({
      next: data => {
        return data;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }

}
