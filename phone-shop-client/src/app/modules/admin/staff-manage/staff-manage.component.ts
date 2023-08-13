import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { AdminService } from '../admin.service';
import { UserService } from '../../user/user.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-staff-manage',
  templateUrl: './staff-manage.component.html',
  styleUrls: ['./staff-manage.component.scss']
})
export class StaffManageComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  dataSource: MatTableDataSource<any>;
  pageSizeOption = CONFIG.PAGING_OPTION;
  pageSize = this.pageSizeOption[0];
  pageLength = 100;
  displayedColumns = ['image', 'name', 'email', 'phone', 'age', 'edit'];
  constructor(
    private loader: LoadingService,
    private notify: NotificationService,
    private userService: UserService,
    private adminService: AdminService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userInfo: UserInfoService,
    private dialogService: OpenDialogService,
  ) {
    this.dataSource = new MatTableDataSource<any>();
  }

  ngOnInit() {
    this.getAllUser();
  }
  getAllUser() {
    this.loader.showProgressBar();
    this.userService.getAllUser()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.dataSource.data = response.data;
          this.dataSource.paginator = this.paginator;
          this.pageLength = this.dataSource.data.length;
        }
      })
  }
}
