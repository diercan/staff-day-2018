import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { ToasterService } from 'angular2-toaster';

import { Client } from './client.contract';
import { ClientsService } from './clients.service';
import { CollectionResource, SingleResource } from 'media-types/common';

@Injectable()
export class ClientsToastDecoratorService implements ClientsService {
  constructor(private _clients: ClientsService, private _toasts: ToasterService) {}

  saveClient(client: SingleResource<Client>): Promise<Response> {
    return this._clients
      .saveClient(client)
      .then(r => this._showSuccessToast(r, 'Client added'))
      .catch(e => this._showErrorToast(e));
  }

  getClients(href: string): Promise<CollectionResource<Client>> {
    return this._clients.getClients(href).catch(e => this._showErrorToast(e));
  }

  getClient(href: string): Promise<SingleResource<Client>> {
    return this._clients.getClient(href).catch(e => this._showErrorToast(e));
  }

  removeClient(client: SingleResource<Client>): Promise<Response> {
    return this._clients
      .removeClient(client)
      .then(r => this._showSuccessToast(r, 'Client removed'))
      .catch(e => this._showErrorToast(e));
  }

  private _showSuccessToast(response: Response, message: string): Response {
    this._toasts.clear();
    this._toasts.pop('success', message);
    return response;
  }

  private _showErrorToast<TError extends Response | string | Object>(error: TError): Promise<typeof error> {
    const message =
      error instanceof Response ? error.json().message : typeof error !== 'string' ? JSON.stringify(error) : error;

    this._toasts.clear();
    this._toasts.pop('error', 'Oops', message);

    return Promise.reject(error);
  }
}
