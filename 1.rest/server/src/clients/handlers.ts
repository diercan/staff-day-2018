import * as uuid from 'node-uuid';
import { Request, Response, Next } from 'restify';

import { ClientError } from '../core';
import { Clients, NotFoundClient } from './repository';
import { Client } from './domain';
import { ResourceRequest } from '../extensions';
import { toClientsWithHypermedia, toClientWithHypermedia, toClientTemplateWithHypermedia } from './hypermedia';

export const addClient = (clients: Clients) => (req: Request, res: Response, next: Next) => {
    if (!req.body) {
        return error(res, next, new ClientError("Request's body is missing"));
    }

    try {
        const client = Client.fromObject({ ...req.body, id: uuid.v4() });
        clients.add(client);
        return success(res, next);
    } catch (e) {
        return error(res, next, e);
    }
};

export const addOrUpdateClient = (clients: Clients) => (req: Request, res: Response, next: Next) => {
    if (!req.body) {
        return error(res, next, new ClientError("Request's body is missing"));
    }

    try {
        const client = Client.fromObject({ ...req.body, id: req.params.id });
        clients.addOrUpdate(client);
        return success(res, next);
    } catch (e) {
        return error(res, next, e);
    }
};

export const removeClient = (clients: Clients) => (req: Request, res: Response, next: Next) => {
    try {
        clients.remove(req.params.id);
        return success(res, next);
    } catch (e) {
        /*
                According to RFC7231#4.2.3 DELETE should be idempotent. As such, if
                a client cannot be removed because it doesn't exist, we return a 204
                (no content) instead of a client error.
            */
        if (e instanceof NotFoundClient) {
            return success(res, next);
        } else {
            return error(res, next, e);
        }
    }
};

export const getClients = (clients: Clients) => (req: ResourceRequest<Client[]>, res: Response, next: Next) => {
    try {
        req.withHypermedia = toClientsWithHypermedia;
        return success(res, next, clients.get());
    } catch (e) {
        return error(res, next, e);
    }
};

export const getClientTemplate = () => (req: ResourceRequest<Client>, res: Response, next: Next) => {
    try {
        req.withHypermedia = toClientTemplateWithHypermedia;
        return success(res, next, {});
    } catch (e) {
        return error(res, next, e);
    }
};

export const getClient = (clients: Clients) => (req: ResourceRequest<Client>, res: Response, next: Next) => {
    try {
        req.withHypermedia = toClientWithHypermedia
        return success(res, next, clients.get(req.params.id));
    } catch (e) {
        return error(res, next, e);
    }
};

function success(res: Response, next: Next, body?: Object, statusCode?: number): void {
    res.send(statusCode || (body === undefined ? 204 : 200), body === undefined ? undefined : JSON.stringify(body));
    next();
}

function error(res: Response, next: Next, err: Object): void {
    if (err instanceof NotFoundClient) {
        res.send(404, err);
    } else if (err instanceof ClientError) {
        res.send(400, err);
    } else {
        /*
            Errors that are instances of a class not inheriting from ClientError may
            contain highly sensitive information and, as such, should not be
            included in the response. In a production environment, those errors
            would natuarally be logged and a unique identifier of the logged error
            would be included in the response so that client errors can be traced
            back to errors in the error log.
        */
        res.send(500);
    }

    next();
}
