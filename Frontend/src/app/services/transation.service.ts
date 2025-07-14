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

  createTransation(transation: Transation) {
    return this.httpClient.post<Transation>(
      `${this._url}/transation`,
      transation
    );
  }

  getTransation(personId: string, page: number, limit: number) {
    return this.httpClient.get<Transation[]>(
      `${this._url}/transation/${personId}/${page}/${limit}`
    );
  }

  deleteTransation(transationId: string) {
    return this.httpClient.delete<Transation>(
      `${this._url}/transation/${transationId}`
    );
  }

  patchTransation(transation: Transation, transationId: string) {
    return this.httpClient.patch(
      `${this._url}/transation/${transationId}`,
      transation
    );
  }
}
