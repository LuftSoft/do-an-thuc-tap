import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductDetailComponent } from '../product/product-detail/product-detail.component';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { BrandDetailComponent } from '../product/brand-detail/brand-detail.component';
import { AdminService } from '../admin.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { MatTableDataSource } from '@angular/material/table';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { MatPaginator } from '@angular/material/paginator';
import { finalize, pipe } from 'rxjs';

@Component({
  selector: 'app-order-manage',
  templateUrl: './order-manage.component.html',
  styleUrls: ['./order-manage.component.scss']
})
export class OrderManageComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  dataSource: MatTableDataSource<any>;
  pageSizeOption = CONFIG.PAGING_OPTION;
  pageSize = this.pageSizeOption[0];
  pageLength = 100;
  displayedColumns = ['createDate', 'paymentMethod', 'paymentStatus', 'orderStatus', 'total_product', 'edit'];
  constructor(
    private adminService: AdminService,
    private loader: LoadingService,
    private dialog: OpenDialogService,
    private notify: NotificationService
  ) {
    this.dataSource = new MatTableDataSource<any>();
  }

  ngOnInit() {
    this.loader.showProgressBar();
    this.adminService.getAllOrder()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.dataSource.data = response.data;
          this.dataSource.paginator = this.paginator;
          this.pageLength = this.dataSource.data.length;
        }
      })
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
    this.loader.showProgressBar()
    this.adminService.updateOrderStatus({
      orderId: data.id,
      status: CONFIG.ORDER.STATUS.PREPARED
    })
      .pipe(finalize(() => { this.loader.hideProgressBar(); this.ngOnInit(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('confirm order success');
        }
      });
  }
  onConfirmOrderDelivery(data: any) {
    this.loader.showProgressBar()
    this.adminService.updateOrderStatus({
      orderId: data.id,
      status: CONFIG.ORDER.STATUS.DELIVERY
    })
      .pipe(finalize(() => { this.loader.hideProgressBar(); this.ngOnInit() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('confirm order success');
        }
      });
  }
  onConfirmOrderPayment(data: any) {
    this.loader.showProgressBar()
    this.adminService.updateOrderPaymentStatus({
      orderId: data.id,
      paymentMethod: data.paymentMethod,
      paymentStatus: CONFIG.ORDER.PAYMENT.STATUS.PAID
    })
      .pipe(finalize(() => { this.loader.hideProgressBar(); this.ngOnInit(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('confirm order success');
        }
      });;
  }
  onCancelOrder(id: any) {
    this.loader.showProgressBar();
    this.adminService.updateOrderStatus({
      orderId: id,
      status: CONFIG.ORDER.STATUS.CANCELED
    })
      .pipe(finalize(() => { this.loader.hideProgressBar(); this.ngOnInit(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('confirm order success');
        }
      });;
  }
  onPaging() { }
  onOrderDetail(data: any) {
    this.dialog.openDialog(OrderDetailComponent,
      { type: 'view', data: data, title: 'Chi tiết sản phẩm' }, '75vw', '70vh')
  }

  onBrandDetail(brand: any) {
    this.dialog.openDialog(BrandDetailComponent, { type: 'view' }, '75vw', '70vh')
  }
  onEditProductDetail(data: any) {
    this.dialog.openDialog(ProductDetailComponent,
      { type: 'edit', data: data, title: 'Chỉnh sửa sản phẩm' }, '75vw', '70vh')
      .afterClosed().subscribe(response => {
        if (response) {
          this.ngOnInit();
        }
      })
  }
  onDeleteProductDetail(id: string) {
    this.dialog.openDialog(ConfirmDialogComponent, {}, '300px', '200px')
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
    this.dialog.openDialog(ProductDetailComponent,
      { type: 'add', title: 'Thêm sản phẩm' }, '75vw', '70vh')
      .afterClosed().subscribe(response => {
        if (response) {
          this.ngOnInit();
        }
      })
  }
  onCreateWarehouseTicket() {

  }
  paymentStatusClass(status: any) {
    let result = '';
    switch (status) {
      case CONFIG.ORDER.PAYMENT.STATUS.CONFIRMING:
        result = 'tag-confirming';
        break;
      case CONFIG.ORDER.PAYMENT.STATUS.PAID:
        result = 'tag-paid';
        break;
      case CONFIG.ORDER.PAYMENT.STATUS.UNPAID:
        result = 'tag-unpaid';
        break;
    }
    return result;
  }
  paymentMethodClass(method: any) {
    let result = '';
    switch (method) {
      case CONFIG.ORDER.PAYMENT.METHOD.OFFLINE:
        result = 'tag-offline';
        break;
      case CONFIG.ORDER.PAYMENT.METHOD.ONLINE:
        result = 'tag-online';
        break;
    }
    return result;
  }
  getNewestStatus(order: any) {
    let newestStatus = order[0];
    for (let status of order) {
      if (new Date(status.created) > new Date(newestStatus.created)) newestStatus = status;
    }
    return newestStatus.status.statusType;
  }
  orderStatusClass(status: any) {
    let newestStatus = this.getNewestStatus(status);
    let result = '';
    switch (newestStatus) {
      case CONFIG.ORDER.STATUS.CREATED:
        result = 'tag-confirmed';
        break;
      case CONFIG.ORDER.STATUS.PREPARED:
        result = 'tag-prepared';
        break;
      case CONFIG.ORDER.STATUS.DELIVERY:
        result = 'tag-unpaid';
        break;
      case CONFIG.ORDER.STATUS.TRANFERED:
        result = 'tag-paid';
        break;
      case CONFIG.ORDER.STATUS.CANCELED:
        result = 'tag-reject';
        break;
    }
    return result;
  }
}
