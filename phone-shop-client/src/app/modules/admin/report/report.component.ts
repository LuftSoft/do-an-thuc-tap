import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import * as FileSaver from 'file-saver';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { Helpers, NotificationService } from 'src/app/core/service/notification.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { UserService } from '../../user/user.service';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  dataSource: MatTableDataSource<any>;
  day: any = 2023;
  pageSizeOption = CONFIG.PAGING_OPTION;
  pageSize = this.pageSizeOption[0];
  pageLength = 100;
  year = [2020, 2021, 2022, 2023];
  displayedColumns = ['date', 'orderCount', 'productCount', 'total'];
  statTypes = ['day', 'month']
  statType = 'day'
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
    this.getDayStat();
    // this.getMonthStat(2023);
  }
  getDayStat() {
    this.loader.showProgressBar();
    this.adminService.getDayReport()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe(response => {
        this.dataSource.data = response.data;
        this.dataSource.paginator = this.paginator;
        this.pageLength = this.dataSource.data.length;
      });
  }
  getMonthStat(year: number) {
    this.loader.showProgressBar();
    this.adminService.getMonthReport(year)
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe(response => {
        this.dataSource.data = response.data;
        this.dataSource.paginator = this.paginator;
        this.pageLength = this.dataSource.data.length;
      });
  }
  exportReport(statType: string) {
    switch (statType) {
      case 'day':
        this.loader.showProgressBar();
        this.adminService.exportDayReport()
          .pipe(finalize(() => {
            this.loader.hideProgressBar();
          }))
          .subscribe((response: Blob) => {
            FileSaver.saveAs(response, 'day-report-' + Helpers.getCurrentStringTime() + '.pdf');
          })
        break;
      case 'month':
        this.loader.showProgressBar();
        this.adminService.exportMonthReport(2023)
          .pipe(finalize(() => {
            this.loader.hideProgressBar();
          }))
          .subscribe((response: Blob) => {
            FileSaver.saveAs(response, 'month-report-' + Helpers.getCurrentStringTime() + '.pdf');
          })
        break;

    }
  }
  onChangeReport($event: any) {
    switch ($event) {
      case 'day':
        this.getDayStat();
        break;
      case 'month':
        this.getMonthStat(this.day);
        break;
    }
  }
  onChangeYear(e: any) {
    this.getMonthStat(e);
  }
}
