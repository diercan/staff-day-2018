import { Server } from 'restify';

import * as handlers from './handlers';
import { Client, Country } from './domain';
import { Clients } from './repository';

export default function bootstrap(server: Server) {
    const clients = getClientsRepository();

    server.post('/api/clients', handlers.addClient(clients));
    server.put('/api/clients/:id', handlers.addOrUpdateClient(clients));
    server.del('/api/clients/:id', handlers.removeClient(clients));
    server.get('/api/clients', handlers.getClients(clients));
    server.get('/api/clients/template', handlers.getClientTemplate());
    server.get('/api/clients/:id', handlers.getClient(clients));
}

function getClientsRepository(): Clients {
    const clients = new Clients();

    clients.add(new Client('a51e95ed-ae46-4010-8784-8e11cb4e346d', 'Robert', 'Ford', Country.UnitedKingdom, 'GL1 1AP'));
    clients.add(new Client('a1afa68a-53e7-4925-9a29-ee539434cb17', 'Phillip', 'Meyer', Country.Germany, '13189'));

    return clients;
}
