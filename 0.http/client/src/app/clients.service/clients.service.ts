import { Response } from '@angular/http';

import { Client } from './client.contract';

export abstract class ClientsService {
  abstract getClientTemplate(): Promise<Client>;
  abstract addClient(client: Client): Promise<Response>;
  abstract updateClient(client: Client): Promise<Response>;
  abstract getClients(): Promise<Client[]>;
  abstract getClient(id: string): Promise<Client>;
  abstract removeClient(id: string): Promise<Response>;
}
