import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { Product } from '../models/interfaces';

@Injectable({
  providedIn: 'root',
})
export class ProductListService {
  constructor(private http: HttpClient) {}

  public getProductsBySponsorId(sponsorId: number) {
    return this.http.get<Product[]>(
      `${api_url}/Product/GetProductsBySponsorId/${sponsorId}`
    );
  }

  public postArrayOfTrackIds(sponsorId: number, tracksIds: number[]) {
    const body = {
      sponsorId: sponsorId,
      tracksIds: tracksIds
    };

    this.http.post(`${api_url}/Product/PostArrayOfTrackIds`, body).subscribe({
      next: data => {
        return data;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    });
  }

}
