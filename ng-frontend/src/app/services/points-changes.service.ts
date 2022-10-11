import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { User, PointsChanges } from '../models/interfaces';

@Injectable({
  providedIn: 'root',
})
export class PointsChangesService {
  constructor(private http: HttpClient) {}

  public getTransactions(userID: number) {
    return this.http.get<PointsChanges[]>(
      `${api_url}/PointTrans/GetPointsForUser/${userID}`
    );
  }
}