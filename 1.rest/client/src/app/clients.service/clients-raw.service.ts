import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptionsArgs } from '@angular/http';

import { Client } from './client.contract';
import { ClientsService } from './clients.service';
import { CollectionResource, SingleResource } from 'media-types/common';

@Injectable()
export class ClientsRawService implements ClientsService {
  private readonly _defaultRequestOptions: RequestOptionsArgs = {
    headers: new Headers({
      Accept: 'application/vnd.example+json'
    })
  };

  constructor(private _http: Http) {}

  saveClient(client: SingleResource<Client>): Promise<Response> {
    const link =
      client.links.find(l => l.relation === 'self' && l.allow.includes('create')) ||
      client.links.find(l => l.relation === 'self' && l.allow.includes('update'));
    const method = link.allow.includes('create') ? 'post' : 'put';
    return this._http[method].call(this._http, link.href, client.properties).toPromise();
  }

  getClients(href: string): Promise<CollectionResource<Client>> {
    return this._http
      .get(href, this._defaultRequestOptions)
      .toPromise()
      .then(r => r.json());
  }

  getClient(href: string): Promise<SingleResource<Client>> {
    return this._http
      .get(href, this._defaultRequestOptions)
      .toPromise()
      .then(r => r.json());
  }

  removeClient(client: SingleResource<Client>): Promise<Response> {
    return this._http
      .delete(client.links.find(l => l.relation === 'self' && l.allow.includes('delete')).href)
      .toPromise();
  }
}
