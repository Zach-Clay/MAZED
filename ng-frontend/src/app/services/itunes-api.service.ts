import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { LoginAttempt } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class ItunesApiService {

  constructor(private http: HttpClient) { }

  //Returns an Array of products with the term=term and media=media
  public getProducts(term: string, media: string) {
    return this.http.get(`${api_url}/SponsorQueryParams/GetMediaTerm?term=${term}&media=${media}`);
  }

  

}
