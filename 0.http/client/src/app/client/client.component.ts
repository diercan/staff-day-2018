import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { Client, ClientsService } from '../clients.service';

@Component({
  selector: 'ui-client',
  templateUrl: './client.component.html'
})
export class ClientComponent implements OnInit {

  model: Observable<Client>;

  constructor(private _route: ActivatedRoute, private _router: Router, private _clients: ClientsService) {
  }

  ngOnInit(): void {
    this.model = this
      ._route
      .params
      .pipe(switchMap(route => route.id ? this._clients.getClient(route.id) : this._clients.getClientTemplate()));
  }

  save(client: Client): void {
    (client.id
      ? this._clients.updateClient(client)
      : this._clients.addClient(client))
      .then(() => this._router.navigate(['../']));
  }
}
