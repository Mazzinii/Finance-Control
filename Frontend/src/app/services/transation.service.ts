import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Transation } from '../models/transation.model';

@Injectable({
  providedIn: 'root',
})
export class TransationService {
  private _url = environment.api;

  private httpClient = inject(HttpClient);

  createTransation(transation: Transation, headObj: HttpHeaders) {
    return this.httpClient.post<Transation>(
      `${this._url}/transation`,
      transation,
      { headers: headObj }
    );
  }

  getTransation(
    personId: string,
    page: number,
    limit: number,
    headObj: HttpHeaders
  ) {
    return this.httpClient.get<Transation[]>(
      `${this._url}/transation/${personId}/${page}/${limit}`,
      { headers: headObj }
    );
  }

  deleteTransation(transationId: string, headObj: HttpHeaders) {
    return this.httpClient.delete<Transation>(
      `${this._url}/transation/${transationId}`,
      { headers: headObj }
    );
  }

  patchTransation(
    transation: Transation,
    transationId: string,
    headObj: HttpHeaders
  ) {
    return this.httpClient.patch(
      `${this._url}/transation/${transationId}`,
      transation,
      { headers: headObj }
    );
  }
}
