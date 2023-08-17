import { Component, OnInit } from '@angular/core';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { UserService } from '../../user/user.service';
import { AdminService } from '../admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public dashboard: any = {};
  phones = {
    apple: 5,
    nokia: 2,
    samsung: 6,
    xiaomi: 6,
    oneplus: 2,
    realme: 2,
    oppo: 2,
    vivo: 2
  }
  mock = [1, 2, 3, 4, 5]
  constructor(
    private loader: LoadingService,
    private notify: NotificationService,
    private userService: UserService,
    private adminService: AdminService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userInfo: UserInfoService,
    private dialogService: OpenDialogService,
  ) { }

  ngOnInit() {
    this.getDashBoard();
  }
  getDashBoard() {
    this.loader.showProgressBar();
    this.adminService.getDashBoard()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe(response => {
        this.dashboard = response.data;
      });
  }
}
