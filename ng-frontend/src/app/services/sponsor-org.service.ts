import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { SponsorOrg } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class SponsorOrgService {

  constructor(private http: HttpClient) { }

  public getAllOrgs() {
    return this.http.get<SponsorOrg[]>(`${api_url}/SponsorOrg`);
  }

  public getSponsorOrg(id: number) {
    return this.http.get<SponsorOrg>(`${api_url}/SponsorOrg/${id}`);
  }

  public updateSponsorOrg(id: number, org: SponsorOrg) {
    return this.http.put<SponsorOrg>(`${api_url}/SponsorOrg/${id}`, org).subscribe({
      next: (data) => {
        return data;
      },
      error: (error) => {
        console.error('There was an error!', error);
      },
    });
  }

}
