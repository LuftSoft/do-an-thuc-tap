import { Injectable, OnInit, EventEmitter } from '@angular/core';
import { Helpers, NotificationService } from './notification.service';
import { CONFIG } from '../constant/CONFIG';
import { BehaviorSubject, Observable, finalize, pipe } from 'rxjs'
import { UserService } from 'src/app/modules/user/user.service';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  user: any = {};
  cart: any[] = [];
  order: any[] = [];
  eventReload: BehaviorSubject<any>;
  userinfo$: Observable<any>;
  constructor(
    private userService: UserService,
    private notity: NotificationService,
    private load: LoadingService
  ) {
    this.getUserInfo();
    this.eventReload = new BehaviorSubject<any>(this.user);
    this.userinfo$ = this.eventReload.asObservable();
  }
  sendUserInfo() {
    this.eventReload.next(this.getUserInfo());
  }
  resetOrder() {
    this.order = [];
  }
  updateUserInfo() {
    this.load.showProgressBar()
    this.userService.getUserInfo()
      .pipe(finalize(() => { this.load.hideProgressBar() }))
      .subscribe((response: any) => {
        localStorage.setItem('user', response);
      });
  }

  getUserInfo() {
    if (localStorage.getItem(CONFIG.AUTH.USER)) {
      this.user = Helpers.parse(localStorage.getItem(CONFIG.AUTH.USER));
    }
    else {
      this.user = null;
    }
  }

}
