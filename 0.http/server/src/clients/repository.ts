import { ClientError } from "../core";
import { Client } from './domain';

export class Clients {

    private _clients: { [key: string]: Client } = { };

    add(client: Client): void {
        if (!this._clients[client.id]) {
            this._clients[client.id] = client;
        } else {
            throw new DuplicateClient(client);
        }
    }

    addOrUpdate(client: Client): void {
        this._clients[client.id] = client;
    }

    get(): Client[];
    get(id: string): Client;

    get(id?: string): Client[] | Client {
        if (typeof id !== 'string') {
            return Object.values(this._clients);
        } else if (this._clients[id]) {
            return this._clients[id];
        } else {
            throw new NotFoundClient(id);
        }
    }

    remove(id: string): void {
        if (this._clients[id]) {
            delete this._clients[id];
        } else {
            throw new NotFoundClient(id);
        }
    }
}

export class DuplicateClient extends ClientError {
    constructor(public client: Client) {
        super(`Client #${client.id} already exists`);
    }
}

export class NotFoundClient extends ClientError {
    constructor(public id: string) {
        super(`Client #${id} not found`);
    }
}