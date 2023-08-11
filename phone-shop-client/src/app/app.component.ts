import { Component } from '@angular/core';
import { LoadingService } from './core/service/loading.service';
import { response } from 'express';
import { delay } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'phone-shop';
  loading$: any;
  constructor(private loader: LoadingService) {
    loader.loading$
      .pipe(delay(0))
      .subscribe((response) => {
        this.loading$ = response;
      })
  }
}
