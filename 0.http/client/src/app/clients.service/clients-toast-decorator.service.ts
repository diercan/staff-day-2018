import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { ToasterService } from 'angular2-toaster';

import { Client } from './client.contract';
import { ClientsService } from './clients.service';

@Injectable()
export class ClientsToastDecoratorService implements ClientsService {
    constructor(private _clients: ClientsService, private _toasts: ToasterService) {
    }

    getClientTemplate(): Promise<Client> {
        return this._clients.getClientTemplate()
            .catch(e => this._showErrorToast(e));
    }

    addClient(client: Client): Promise<Response> {
        return this._clients.addClient(client)
            .then(r => this._showSuccessToast(r, 'Client added'))
            .catch(e => this._showErrorToast(e));
    }

    updateClient(client: Client): Promise<Response> {
        return this._clients.updateClient(client)
            .then(r => this._showSuccessToast(r, 'Client updated'))
            .catch(e => this._showErrorToast(e));
    }

    getClients(): Promise<Client[]> {
        return this._clients.getClients()
            .catch(e => this._showErrorToast(e));
    }

    getClient(id: string): Promise<Client> {
        return this._clients.getClient(id)
            .catch(e => this._showErrorToast(e));
    }

    removeClient(id: string): Promise<Response> {
        return this._clients.removeClient(id)
            .then(r => this._showSuccessToast(r, 'Client removed'))
            .catch(e => this._showErrorToast(e));
    }

    private _showSuccessToast(response: Response, message: string): Response {
        this._toasts.clear();
        this._toasts.pop('success', message);
        return response;
    }

    private _showErrorToast<TError extends Response | string | Object>(error: TError): Promise<typeof error> {
        const message = error instanceof Response
            ? error.json().message
            : typeof error !== 'string'
            ? JSON.stringify(error)
            : error;

        this._toasts.clear();
        this._toasts.pop('error', 'Oops', message);

        return Promise.reject(error);
    }
}
