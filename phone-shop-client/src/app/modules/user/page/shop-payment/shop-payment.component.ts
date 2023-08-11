import { Component, OnInit } from '@angular/core';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { UserAddressDialogComponent } from '../user-address-dialog/user-address-dialog.component';

@Component({
  selector: 'app-shop-payment',
  templateUrl: './shop-payment.component.html',
  styleUrls: ['./shop-payment.component.scss']
})
export class ShopPaymentComponent implements OnInit {
  order: any[] = [];
  total_price = 0;
  ship_cost = 30000;
  constructor(
    private userInfoService: UserInfoService,
    private dialogService: OpenDialogService
  ) { }
  onOrder() {

  }
  onChangeAddress() {
    this.dialogService.openDialog(UserAddressDialogComponent, {}, '', '')
      .afterClosed().subscribe(((data) => {
        console.log(data);
      }))
  }
  ngOnInit(): void {
    this.order = this.userInfoService.order;
    console.log(this.userInfoService.order);
  }

}
