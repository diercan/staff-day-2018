import { Routes } from '@angular/router';

import { ClientsComponent } from 'app/clients/clients.component';
import { ClientComponent } from 'app/client/client.component';

export const ROUTES: Routes = [
    { path: 'clients/new', component: ClientComponent },
    { path: 'clients/:id', component: ClientComponent },
    { path: 'clients', component: ClientsComponent },
    { path: '**', redirectTo: 'clients', pathMatch: 'full' }
];
