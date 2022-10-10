import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { User } from '../models/interfaces';

@Injectable({
  providedIn: 'root',
})
export class ProductListService {
  constructor(private http: HttpClient) {}
}
