import { Injectable, Pipe, PipeTransform } from '@angular/core';
import { OpenDialogService } from './dialog/opendialog.service';
import { LoadingServiceComponent } from './loading-service/loading-service.component';
import { BehaviorSubject } from 'rxjs';
@Injectable({
    providedIn: 'root',
})
export class LoadingService {
    private _loading = new BehaviorSubject<boolean>(false);
    public loading$ = this._loading.asObservable();
    showProgressBar() {
        this._loading.next(true);
    }
    hideProgressBar() {
        this._loading.next(false);
    }
}

