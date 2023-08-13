import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { Helpers, NotificationService } from 'src/app/core/service/notification.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { finalize, delay } from 'rxjs'
import { LoadingService } from 'src/app/core/service/loading.service';

@Component({
  selector: 'app-shop-nav',
  templateUrl: './shop-nav.component.html',
  styleUrls: ['./shop-nav.component.scss']
})
export class ShopNavComponent implements OnInit {
  user: any;
  constructor(
    private notify: NotificationService,
    private router: Router,
    private userInfoService: UserInfoService,
    private loader: LoadingService
  ) {
    this.userInfoService.eventReload.subscribe(data => {
      this.user = Helpers.clonDeep(data);
    });
  }

  ngOnInit(): void {
    this.user = this.userInfoService.user;
    console.log(this.user);
  }
  openNotify() {
    this.notify.openSnackBar("test snack bar", "close");
  }
  onLogout() {
    localStorage.removeItem(CONFIG.AUTH.USER_ACCESS_TOKEN);
    localStorage.removeItem(CONFIG.AUTH.USER_REFRESH_TOKEN);
    localStorage.removeItem(CONFIG.AUTH.USER);
    this.notify.notifySuccess("Đăng xuất thành công.");
    this.router.navigateByUrl('/')
      .then(() => {
        window.location.reload();
      });
  }
}
