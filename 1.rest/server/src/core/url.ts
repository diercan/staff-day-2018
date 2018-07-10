import { Request } from 'restify';
import * as url from 'url';
import { guardAgainstEmptyArgument } from './guards';

export class Url {
    constructor(private _url: string) {
        guardAgainstEmptyArgument(() => _url);
    }

    toString(): string {
        return this._url;
    }
}

export function pathNameToUrl(req: Request, pathName: string): Url {
    return new Url(
        url.format({
            protocol: req.isSecure() ? 'https' : 'http',
            host: req.headers.host,
            pathname: pathName
        })
    );
}

export function getBaseAddressFromRequest(req: Request): Url{
    return new Url(
        url.format({
            protocol: req.isSecure() ? 'https' : 'http',
            host: req.headers.host
        })
    );
}
