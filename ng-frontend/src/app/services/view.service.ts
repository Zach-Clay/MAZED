import { Injectable } from '@angular/core';
import { User } from '../models/interfaces';

@Injectable({
  providedIn: 'root',
})
export class ViewService {
  curentViewingUser!: User;

  constructor() {}

  setCurrentUser(u: User) {
    this.curentViewingUser = u;
  }

  getCurrentUser() {
    return this.curentViewingUser;
  }
}
