import * as restify from 'restify';
import * as cors from 'restify-cors-middleware';

import { default as bootstrapAssets } from './assets/bootstrap';
import { default as bootstrapClients } from './clients/bootstrap';
import { ResourceRequest } from './extensions';
import { pathNameToUrl } from './core';

const server = restify.createServer({
    formatters: {
        'application/vnd.example+json': <T>(req: ResourceRequest<T>, res: restify.Response, data: string) => {
            const body = req.withHypermedia(req, res, JSON.parse(data));
            return JSON.stringify(body, undefined, 2);
        },
        'application/json': (req: restify.Request, res: restify.Response, data: string) =>
            JSON.stringify(JSON.parse(data), undefined, 2)
    }
});

/*
    Enable CORS
*/
const { preflight, actual } = cors({
    allowHeaders: ['*'],
    exposeHeaders: ['*'],
    origins: ['*']
});
server.pre(preflight);
server.use(actual);

/*
    Add null withHypermedia implementation
*/
server.pre((req: ResourceRequest<object>, res: restify.Response, next: restify.Next) => {
    req.withHypermedia = (req, res, resource) => ({
        properties: resource,
        links: [
            {
                relation: 'self',
                href: pathNameToUrl(req, req.url || '').toString(),
                allow: ['read']
            }
        ]
    });
    next();
});

/*
    Enable body parsing
*/
server.use(restify.plugins.bodyParser());

/*
    Start server
*/
server.listen(8080, function() {
    console.log('%s listening at %s', server.name, server.url);

    /*
    Bootstrap handlers
    */
    bootstrapAssets(server);
    bootstrapClients(server);
});
