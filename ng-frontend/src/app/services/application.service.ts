import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { Application } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http: HttpClient) { }

  public getApplicationBySponsor(sponsorId: number) {
    
  }

  public getApplicationByUser(userId: number) {
    
  }

  public submitApplication(application: Application) {
    
  }

}
