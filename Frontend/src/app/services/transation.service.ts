import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Transation } from '../models/transation.model';

@Injectable({
  providedIn: 'root',
})
export class TransationService {
  private _url = environment.api;

  private httpClient = inject(HttpClient);

  getTransation(personId: string, page: number, limit: number) {
    return this.httpClient.get<Transation[]>(
      `${this._url}/transation/${personId}/${page}/${limit}`
    );
  }
}
