import { Component, OnInit } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-loading-service',
  templateUrl: './loading-service.component.html',
  styleUrls: ['./loading-service.component.scss']
})
export class LoadingServiceComponent implements OnInit {
  private _loading = new BehaviorSubject<boolean>(false);
  public loading$ = this._loading.asObservable();
  constructor() { }

  ngOnInit() {
  }
  showProgressBar(){
    this._loading.next(true);
  }
  hideProgressBar(){
    this._loading.next(false);
  }
}
