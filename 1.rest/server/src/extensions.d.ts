import { Request, Response, Next } from "restify";
import { Resource } from "media-types";

interface ResourceRequest<T> extends Request {
    withHypermedia(req: Request, res: Response, resource: T): Resource<T>;
}