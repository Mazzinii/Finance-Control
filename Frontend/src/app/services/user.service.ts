import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../models/user.model';
import { UserLogin } from '../models/userLogin.model';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  private _url = environment.api;

  private httpClient = inject(HttpClient);

  getUsers() {
    return this.httpClient.get<User[]>(this._url + '/user?page=1&limit=10');
  }

  createUser(user: User) {
    return this.httpClient.post<User>(`${this._url}/user`, user);
  }

  login(userLogin: UserLogin) {
    return this.httpClient.post<User>(`${this._url}/user/login`, userLogin);
  }
}
