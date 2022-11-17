import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { Order } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }

  public getOrdersByDriverId(userId: number) {
    return this.http.get<Order[]>(`${api_url}/DriverOrder/GetOrdersByDriverId/${userId}`);
  }

  public deleteOrder(orderId: number) {
    return this.http.delete(`${api_url}/DriverOrder/${orderId}`);
  }

}
