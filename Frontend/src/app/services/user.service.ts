import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../models/user.model';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  private _url = environment.api;

  constructor(private httpClient: HttpClient) {}

  getUsers() {
    return this.httpClient.get<User[]>(this._url + '/User?page=1&limit=10');
  }

  createUser(user: User) {
    return this.httpClient.post<User>(`${this._url}/User`, user);
  }
}
