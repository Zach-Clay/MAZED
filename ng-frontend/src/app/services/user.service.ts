import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { User, SponsorOrg, UserToSponsor } from '../models/interfaces';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

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
      next: (data) => {
        return data;
      },
      error: (error) => {
        console.error('There was an error!', error);
      },
    });
  }

  //Update user's information
  public updateUser(id: number, user: User) {
    this.http.put(`${api_url}/user/${id}`, user).subscribe({
      next: (data) => {
        return data;
      },
      error: (error) => {
        console.error('There was an error!', error);
      },
    });
  }

  //Get the sponsor org for a sponsor user --> they cannot have more that one org!!
  public getSponsorOrgBySponsorUserId(sponsorUserId: number) {
    return this.http.get<SponsorOrg>(`${api_url}/userToSponsor/GetSponsorOrgFromSponsorUsersId/${sponsorUserId}`);
  }

  //Get sponsor orgs by user id
  public getSponsorOrgsByDriverUserId(driverId: number) {
    return this.http.get<SponsorOrg[]>(`${api_url}/userToSponsor/GetSponsorsOrgsFromDriverUsersId/${driverId}`);
  }

  //get all the sponsor users for a given sponsorOrgId
  public getSponsorUsersBySponsorOrgId(sponsorOrgId: number) {
    return this.http.get<User[]>(`${api_url}/userToSponsor/GetSponsorsBySponsorId/${sponsorOrgId}`);
  }

   //Get all driver users for a sponsor
   public getDriverUsersBySponsorOrgId(sponsorOrgId: number) {
    return this.http.get<User[]>(`${api_url}/userToSponsor/GetDriversBySponsorId/${sponsorOrgId}`);
  }

  //Get the userToSponsor entries for a given driver user
  public getUserToSponsorEntriesByDriverUsersId(driverUsersId: number) {
    return this.http.get<UserToSponsor[]>(`${api_url}/userToSponsor/GetDriversEntriesFromDriverUsersId/${driverUsersId}`);
  }

  //Add a user to a sponsor's org
  public postUserToSponsor(userToSponsor: UserToSponsor) {
    this.http.post(`${api_url}/userToSponsor`, userToSponsor).subscribe({
      next: (data) => {
        return data;
      },
      error: (error) => {
        console.error('There was an error!', error);
      },
    });
  }

  //remove a user from a sponsor org given the userToSponsor ID
  public removeUserFromSponsorOrg(userToSponsorId: number) {
    this.http.delete(`${api_url}/userToSponsor/${userToSponsorId}`).subscribe({
      next: (data) => {
        return data;
      },
      error: (error) => {
        console.error('There was an error!', error);
      },
    });
  }
  
}
