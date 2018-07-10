import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of as ObservableOf } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { SingleResource } from 'media-types/common';

import { Client, ClientsService } from '../clients.service';

@Component({
  selector: 'ui-client',
  templateUrl: './client.component.html'
})
export class ClientComponent implements OnInit {
  resource: Observable<SingleResource<Client>>;

  constructor(private _route: ActivatedRoute, private _router: Router, private _clients: ClientsService) {}

  ngOnInit(): void {
    this.resource = this._route.params.pipe(switchMap(route => this._clients.getClient(route.href)));
  }

  save(client: SingleResource<Client>): void {
    this._clients.saveClient(client).then(() => this._router.navigate(['../']));
  }
}
