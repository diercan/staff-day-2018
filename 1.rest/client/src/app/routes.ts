import { Routes } from '@angular/router';

import { ClientsComponent } from 'app/clients/clients.component';
import { ClientComponent } from 'app/client/client.component';
import { BASE_URL } from './settings';

export const ROUTES: Routes = [
    { path: 'edit/:href', component: ClientComponent },
    { path: 'list/:href', component: ClientsComponent },
    { path: '**', redirectTo: `list/${encodeURIComponent(BASE_URL)}`, pathMatch: 'full' }
];
