import { Component } from '@angular/core';
import { LoadingService } from './core/service/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'phone-shop';
  loading$ = this.loader.loading$;
  constructor(private loader : LoadingService){}
}
