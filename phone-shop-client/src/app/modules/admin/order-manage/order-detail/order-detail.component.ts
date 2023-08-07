import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { NotificationService } from 'src/app/core/service/notification.service';
import { AdminService } from '../../admin.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {

  form: FormGroup;
  brandList: any = [];
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private adminService: AdminService,
    private notify: NotificationService
  ) {
    if (data.type === CONFIG.FORM.TYPE.ADD) {
      this.form = new FormGroup({
        createDate: new FormControl(null, Validators.required),
        orderStatus: new FormControl(null, Validators.required),
        orderDetail: new FormControl(null, Validators.required),
        paymentStatus: new FormControl(null, Validators.required),
        paymentMethod: new FormControl(null, Validators.required),
        address: new FormControl(null, Validators.required),
      });
    } else {
      let formData = data.data
      this.form = new FormGroup({
        createDate: new FormControl(formData.createDate, Validators.required),
        orderStatus: new FormControl(formData.orderStatus, Validators.required),
        orderDetail: new FormControl(formData.orderDetail, Validators.required),
        paymentStatus: new FormControl(formData.paymentStatus, Validators.required),
        paymentMethod: new FormControl(formData.paymentMethod, Validators.required),
        address: new FormControl(formData.address, Validators.required)
      });
    }
  }

  ngOnInit() {
    this.getAllFilter();
  }
  onUpdate() {
    this.adminService.updateProduct(this.form.getRawValue())
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('Cập nhật sản phẩm thành công!');
        }
      });
  }
  onCreate() {
    console.log(this.form.getRawValue());
    this.adminService.createProduct(this.form.getRawValue())
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('Tạo sản phẩm mới thành công!');
        }
      });
  }

  getBrandFilter() {
    this.adminService.getAllBrand().subscribe((response) => {
      if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
        this.brandList = response.data;
      }
    })
  }
  getAllFilter() {
    this.getBrandFilter();
  }
  getCurrentStatus(data: any) {
    if (data.length === 0) return '--';
    var newEst = data[0];
    for (let i = 0; i < data.length; i++) {
      if (data[i].created > newEst) {
        newEst = data[i];
      }
    }
    return newEst.status.statusType;
  }
  getTotalProduct(data: any) {
    let total = 0;
    data.forEach((element: any) => {
      total += element.quantity
    });
    return total;
  }
  getTotalPrice(data: any) {
    let total = 0;
    data.forEach((element: any) => {
      total += (element.quantity * element.phone.soldPrice);
    });
    return total;
  }
}
