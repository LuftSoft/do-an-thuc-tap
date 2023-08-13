import { Component, OnInit } from '@angular/core';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { UserAddressDialogComponent } from '../user-address-dialog/user-address-dialog.component';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from 'src/app/modules/admin/admin.service';
import { UserService } from '../../user.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { PaySuccessDialogComponent } from '../pay-success-dialog/pay-success-dialog.component';
import { ConfirmDialogComponent } from 'src/app/modules/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-shop-payment',
  templateUrl: './shop-payment.component.html',
  styleUrls: ['./shop-payment.component.scss']
})
export class ShopPaymentComponent implements OnInit {
  order: any[] = [];
  total_price = 0;
  ship_cost = 30000;
  payAddress: any = {};
  paymentMethod: any = 'COD';
  paymentStatus: any = 'UNPAID';
  phones: any[] = [];
  public user: any;
  constructor(
    private loader: LoadingService,
    private notify: NotificationService,
    private userService: UserService,
    private adminService: AdminService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userInfo: UserInfoService,
    private dialogService: OpenDialogService,
    private userInfoService: UserInfoService
  ) {
  }

  ngOnInit(): void {
    this.setOrder();
    this.getUserInfo();
  }

  getUserInfo() {
    this.loader.showProgressBar();
    this.userService.getUserInfo()
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response: any) => {
        this.user = response;
        this.payAddress = this.user.address.filter((ad: any) => ad.isDefault)[0] || {};
        console.log('payad', this.payAddress);
      })
  }
  configOrderInfo() {

  }
  setOrder() {
    this.order = this.userInfoService.order;
    this.order.forEach(item => {
      this.total_price += item.quantity * item.phone.soldPrice;
      this.phones.push({
        phoneId: item.phone.id,
        quantity: item.quantity
      })
    })
  }
  onOrder() {
    this.loader.showProgressBar();
    this.userService.createOrder({
      addressId: this.payAddress.id,
      paymentMethod: this.paymentMethod,
      paymentStatus: this.paymentStatus,
      phones: this.phones
    })
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe(response => {
        console.log(response);
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('Đặt hàng thành công!');
          this.userInfoService.resetOrder();
          this.dialogService.openDialog(PaySuccessDialogComponent, {},
            '50vw', '40vh')
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notify.notifyError(response.message || 'Failed!')
        }
      })
  }
  onChangeAddress() {
    this.dialogService.openDialog(UserAddressDialogComponent, {}, '', '')
      .afterClosed().subscribe(((data) => {
        console.log(data);
      }))
  }
  check() {
    this.dialogService.openDialog(PaySuccessDialogComponent, {},
      '50vw', '40vh')
  }


}
