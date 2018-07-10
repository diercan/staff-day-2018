import { Response } from '@angular/http';

import { Client } from './client.contract';
import { CollectionResource, SingleResource } from 'media-types/common';

export abstract class ClientsService {
  abstract saveClient(client: SingleResource<Client>): Promise<Response>;
  abstract getClients(href: string): Promise<CollectionResource<Client>>;
  abstract getClient(href: string): Promise<SingleResource<Client>>;
  abstract removeClient(client: SingleResource<Client>): Promise<Response>;
}
