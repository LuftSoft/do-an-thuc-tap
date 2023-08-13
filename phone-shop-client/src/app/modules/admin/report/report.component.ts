import { Component, OnInit, ViewChild } from '@angular/core';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { UserService } from '../../user/user.service';
import { AdminService } from '../admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { finalize } from 'rxjs'
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { OrderDetailComponent } from '../order-manage/order-detail/order-detail.component';
import { BrandDetailComponent } from '../product/brand-detail/brand-detail.component';
import { ProductDetailComponent } from '../product/product-detail/product-detail.component';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  dataSource: MatTableDataSource<any>;
  pageSizeOption = CONFIG.PAGING_OPTION;
  pageSize = this.pageSizeOption[0];
  pageLength = 100;
  displayedColumns = ['date', 'orderCount', 'productCount', 'total'];
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
    this.dataSource.data = [
      ['12/08/2023', 11, 22, 2000002900],
      ['13/08/2023', 12, 55, 12900000],
      ['14/08/2023', 9, 23, 22340000],
      ['15/08/2023', 12, 89, 124000000]
    ];
    this.dataSource.paginator = this.paginator;
    this.pageLength = this.dataSource.data.length;
  }
  getOrderStatus(data: any) {
    if (data.length === 0) return '';
    let result = data[0];
    for (let i = 1; i < data.length; i++) {
      if (data[i].created > result.created) result = data[i];
    }
    return result.status.statusType;
  }
  onConfirmOrder(data: any) {
    this.adminService.updateOrderStatus({
      orderId: data.id,
      status: CONFIG.ORDER.STATUS.PREPARED
    }).subscribe((response) => {
      if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
        this.notify.notifySuccess('confirm order success');
      }
    });
  }
  onConfirmOrderPayment(data: any) {
    this.adminService.updateOrderPaymentStatus({
      orderId: data.id,
      paymentMethod: data.paymentMethod,
      paymentStatus: CONFIG.ORDER.PAYMENT.STATUS.PAID
    }).subscribe((response) => {
      if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
        this.notify.notifySuccess('confirm order success');
      }
    });;
  }
  onCancelOrder(id: any) {
    this.adminService.updateOrderStatus({
      orderId: id,
      status: CONFIG.ORDER.STATUS.CANCELED
    }).subscribe((response) => {
      if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
        this.notify.notifySuccess('confirm order success');
      }
    });;
  }
  onPaging() { }
  onOrderDetail(data: any) {
    this.dialogService.openDialog(OrderDetailComponent,
      { type: 'view', data: data, title: 'Chi tiết sản phẩm' }, '75vw', '70vh')
  }

  onBrandDetail(brand: any) {
    this.dialogService.openDialog(BrandDetailComponent, { type: 'view' }, '75vw', '70vh')
  }
  onEditProductDetail(data: any) {
    this.dialogService.openDialog(ProductDetailComponent,
      { type: 'edit', data: data, title: 'Chỉnh sửa sản phẩm' }, '75vw', '70vh')
      .afterClosed().subscribe(response => {
        if (response) {
          this.ngOnInit();
        }
      })
  }
  onDeleteProductDetail(id: string) {
    this.dialogService.openDialog(ConfirmDialogComponent, {}, '300px', '200px')
      .afterClosed().subscribe((response) => {
        if (response) {
          this.adminService.deleteProduct(id).subscribe((response) => {
            if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
              this.notify.notifySuccess('Delete product success');
            }
          });
        }
      })
  }
  onCreateProduct() {
    this.dialogService.openDialog(ProductDetailComponent,
      { type: 'add', title: 'Thêm sản phẩm' }, '75vw', '70vh')
      .afterClosed().subscribe(response => {
        if (response) {
          this.ngOnInit();
        }
      })
  }
  onCreateWarehouseTicket() {

  }
}
