import { Component, OnInit } from '@angular/core';
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

@Component({
  selector: 'app-order-manage',
  templateUrl: './order-manage.component.html',
  styleUrls: ['./order-manage.component.scss']
})
export class OrderManageComponent implements OnInit {

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
    //const load = this.loader.showProgressBar();
    this.adminService.getAllOrder()
      //.pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.dataSource.data = response.data;
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
}
