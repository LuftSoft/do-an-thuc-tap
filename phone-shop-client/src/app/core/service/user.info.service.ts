import { Injectable, OnInit, EventEmitter } from '@angular/core';
import { Helpers } from './notification.service';
import { CONFIG } from '../constant/CONFIG';
import { BehaviorSubject, Observable } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  user: any = {};
  cart: any[] = [];
  order: any[] = [];
  eventReload: BehaviorSubject<any>;
  userinfo$: Observable<any>;
  constructor() {
    this.getUserInfo();
    this.eventReload = new BehaviorSubject<any>(this.user);
    this.userinfo$ = this.eventReload.asObservable();
  }
  sendUserInfo() {
    this.eventReload.next(this.getUserInfo());
  }
  getUserInfo() {
    if (localStorage.getItem(CONFIG.AUTH.USER)) {
      this.user = Helpers.clonDeep(localStorage.getItem(CONFIG.AUTH.USER));
    }
    else {
      this.user = null;
    }
  }

}
