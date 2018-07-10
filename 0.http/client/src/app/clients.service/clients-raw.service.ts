import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { BASE_URL } from '../settings';
import { Client } from './client.contract';
import { ClientsService } from './clients.service';

@Injectable()
export class ClientsRawService implements ClientsService {
  constructor(private _http: Http) {
  }

  getClientTemplate(): Promise<Client> {
    return Promise.resolve({});
  }

  addClient(client: Client): Promise<Response> {
    return this
      ._http
      .post(this._getClientsUrl(), client)
      .toPromise();
  }

  updateClient(client: Client): Promise<Response> {
    return this
      ._http
      .put(this._getClientUrl(client.id), client)
      .toPromise();
  }

  getClients(): Promise<Client[]> {
    return this
      ._http
      .get(this._getClientsUrl())
      .toPromise()
      .then(r => r.json());
  }

  getClient(id: string): Promise<Client> {
    return this
      ._http
      .get(this._getClientUrl(id))
      .toPromise()
      .then(r => r.json());
  }

  removeClient(id: string): Promise<Response> {
    return this
      ._http
      .delete(this._getClientUrl(id))
      .toPromise();
  }

  private _getClientsUrl(): string {
    return `${BASE_URL}/api/clients`;
  }

  private _getClientUrl(id: string): string {
    return `${BASE_URL}/api/clients/${id}`;
  }
}
