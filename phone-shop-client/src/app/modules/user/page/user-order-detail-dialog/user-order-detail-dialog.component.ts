import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { ConfirmDialogComponent } from 'src/app/modules/confirm-dialog/confirm-dialog.component';
import { UserService } from '../../user.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-user-order-detail-dialog',
  templateUrl: './user-order-detail-dialog.component.html',
  styleUrls: ['./user-order-detail-dialog.component.scss']
})
export class UserOrderDetailDialogComponent implements OnInit {

  user: any;
  order: any = {};
  selectedCart: any[] = [];
  total_price: any = 0;
  constructor(
    private loader: LoadingService,
    private notification: NotificationService,
    private userService: UserService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userInfo: UserInfoService,
    notify: NotificationService,
    private dialogService: OpenDialogService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    console.log(this.data);
    this.getOrderDetail(this.data.id);
  }
  getUserInfo() {
    this.loader.showProgressBar();
    this.userService.getUserInfo()
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response: any) => {
        this.user = response;
      })
  }
  getOrderDetail(id: any) {
    this.userService.getDetailOrder(id)
      .subscribe((response) => {
        console.log(response);
        this.order = response.data;
      })
  }
  isOrderStatusCreated(order: any) {
    return this.getNewestStatus(order) === CONFIG.ORDER.STATUS.CREATED
  }
  isOrderStatusCanceled(order: any) {
    return this.getNewestStatus(order) === CONFIG.ORDER.STATUS.CANCELED
  }
  isOrderStatusDelivery(order: any) {
    return this.getNewestStatus(order) === CONFIG.ORDER.STATUS.DELIVERY
  }
  isOrderStatusPrepared(order: any) {
    return this.getNewestStatus(order) === CONFIG.ORDER.STATUS.PREPARED
  }
  isOrderStatusTranfered(order: any) {
    return this.getNewestStatus(order) === CONFIG.ORDER.STATUS.TRANFERED
  }
  getNewestStatus(order: any) {
    let newestStatus = order.orderStatus[0];
    for (let status of order.orderStatus) {
      if (new Date(status.created) > new Date(newestStatus.created)) newestStatus = status;
    }
    return newestStatus.status.statusType;
  }
  getTotalPrice(order: any) {
    let total = 0;
    for (let orderDetail of order.orderDetail) {
      total += orderDetail.quantity * orderDetail.phone.soldPrice;
    }
    return total;
  }
  onCanceled(order: any) {
    this.dialogService.openDialog(ConfirmDialogComponent, {
      content: 'Xác nhận hủy đơn hàng!'
    }, '30vw', '20vh'
    )
      .afterClosed().subscribe((response) => {
        console.log(response);
        if (response) {
          this.loader.showProgressBar();
          this.userService.cancelOrder(order.id)
            .pipe(finalize(() => {
              this.loader.hideProgressBar();
              setTimeout(() => {
                this.ngOnInit();
              }, 100);
            }))
            .subscribe((response) => {
              if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
                this.notification.notifySuccess('Hủy đơn hàng thành công!')
              }
              else if (response.code === CONFIG.STATUS_CODE.ERROR) {
                this.notification.notifyError(response.message || 'Failed!');
              }
            })
        }
      })
  }
  onTranfered(order: any) {
    console.log(order);
    this.userService.updateOrderStatus({
      orderId: order.id,
      status: CONFIG.ORDER.STATUS.TRANFERED
    })
      .pipe(finalize(() => {
        this.loader.hideProgressBar();
        setTimeout(() => {
          this.ngOnInit();
        }, 100);
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notification.notifySuccess('Xác nhận thành công!')
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message || 'Failed!');
        }
      })
  }
  redirectToProductList() {
    this.router.navigateByUrl('/product');
  }
  getProduct(id: string) {
    var returnResult = '';
    return returnResult;
  }
  upDatePrice(cart: any[]) {
    this.total_price = 0;
    for (let item of cart) {
      this.total_price += item.phone.soldPrice * item.quantity;
    }
  }
  inputChange(event: any) {
    let productId = event.target.value;
    if (event.target.checked) {
      this.selectedCart.push(this.getProduct(productId));
    }
    else {
      this.selectedCart = this.selectedCart.filter(item => item.id != productId);
    }
    this.upDatePrice(this.selectedCart);
  }
  isBtnDisabled() {
    return this.total_price === 0;
  }
  redirectToOrder() {
    this.userInfo.order = this.selectedCart;
    this.router.navigateByUrl('/payment');
  }
  ngOnDestroy(): void {
    //    this.loader.hideProgressBar();
  }
  isSelectedInCart(id: any) {
    let selectedElement = this.selectedCart.filter(item => item.id === id);
    if (selectedElement) {
      return selectedElement.length > 0;
    }
    return false;
  }
  onOrderDetail(id: any) {

  }

}
