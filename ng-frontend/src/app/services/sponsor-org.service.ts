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

}
