import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { AdminService } from '../admin.service';
import { UserService } from '../../user/user.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { delay, finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UserInformationComponent } from '../user-information/user-information.component';

@Component({
  selector: 'app-staff-manage',
  templateUrl: './staff-manage.component.html',
  styleUrls: ['./staff-manage.component.scss']
})
export class StaffManageComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  roles: any = [];
  customerRole: any = '';
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
    this.getAllRole();
    this.getAllUser();
  }
  getAllUser() {
    this.loader.showProgressBar();
    this.userService.getAllUser()
      .pipe(finalize(() => { this.loader.hideProgressBar() }), delay(0))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.dataSource.data = response.data.filter((u: any) => !u.role.includes());
          this.dataSource.paginator = this.paginator;
          this.pageLength = this.dataSource.data.length;
        }
      })
  }
  getAllRole() {
    this.loader.showProgressBar();
    this.userService.getAllUser()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.roles = response.data;
          this.customerRole = this.roles.filter((r: any) => r.name == CONFIG.ROLE.CUSTOMER)[0]
        }
      })
  }
  onUserDetail(id: any) {
    this.dialogService.openDialog(UserInformationComponent, { id: id },
      '35vw', '50vh'
    )
      .afterClosed().subscribe(response => {
        console.log(response);
        if (response) {
          this.notify.notifySuccess('Tạo nhà phân phối thành công!');
        }
      });
  }
}
