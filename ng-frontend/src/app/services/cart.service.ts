import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { api_url, environment } from 'src/environments/environment';
import { Cart } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private http: HttpClient) { }

  public getCartByUserId(userId: number) {
    return this.http.get<Cart[]>(`${api_url}/cart/${userId}`);
  }

  public addToCart(cartItem: Cart) {
    return this.http.post<Cart>(`${api_url}/cart`, cartItem);
  }
  
}
