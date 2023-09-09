import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../admin.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { NotificationService } from 'src/app/core/service/notification.service';

@Component({
  selector: 'navbar-cmp',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  user: any = {};
  roles: any[] = [];
  sidebarValue: any = {
    'dashboard': {
      value: 'dashboard',
      isSelected: true
    },
    'product': {
      value: 'product',
      isSelected: false
    },
    'warehouse': {
      value: 'warehouse',
      isSelected: false
    },
    'order': {
      value: 'order',
      isSelected: false
    },
    'staff': {
      value: 'staff',
      isSelected: false
    },
    'promotion': {
      value: 'promotion',
      isSelected: false
    },
    'report': {
      value: 'report',
      isSelected: false
    },
    'user_information': {
      value: 'user-information',
      isSelected: false
    },
  }
  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    private adminService: AdminService,
    private load: LoadingService,
    private notify: NotificationService
  ) {
    let routerArr = router.url.split('/');
    for (let item in this.sidebarValue) {
      this.sidebarValue[item].isSelected = false;
    }
    if (this.sidebarValue[routerArr[routerArr.length - 1]]) {
      this.sidebarValue[routerArr[routerArr.length - 1]].isSelected = true
    }
    else {
      this.sidebarValue['dashboard'].isSelected = true;
    }

  }

  ngOnInit() {
    this.getUserInfor();
  }
  onLogout() {
    this.notify.notifySuccess('Đăng xuất thành công!')
    localStorage.removeItem(CONFIG.AUTH.ADMIN_ACCESS_TOKEN);
    localStorage.removeItem(CONFIG.AUTH.ADMIN_REFRESH_TOKEN);
    localStorage.removeItem(CONFIG.AUTH.ADMIN);
    setTimeout(() => {
      this.router.navigate(['/admin/login']).then(() => {
        window.location.reload();
      });
    }, 100);
  }
  getUserInfor() {
    try {
      this.load.showProgressBar();
      this.adminService.getUserInfo()
        .pipe(finalize(() => { this.load.hideProgressBar() }))
        .subscribe((response: any) => {
          this.user = response;
          this.roles = response.role;
        });
    }
    catch {

    }
  }
  onSideBarSelect(value: string) {
    for (let item in this.sidebarValue) {
      this.sidebarValue[item].isSelected = false;
    }
    this.sidebarValue[value].isSelected = true;
  }
  isAdmin() {
    return this.roles.includes(CONFIG.ROLE.ADMIN);
  }
  isStaff() {
    return this.roles.includes(CONFIG.ROLE.STAFF);
  }
  isAdminOrStaff() {
    return this.roles.includes(CONFIG.ROLE.ADMIN) || this.roles.includes(CONFIG.ROLE.STAFF);
  }
}
