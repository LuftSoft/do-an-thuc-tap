import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { Helpers, NotificationService } from 'src/app/core/service/notification.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { UserService } from '../../user.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { config, finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-new-address-dialog',
  templateUrl: './add-new-address-dialog.component.html',
  styleUrls: ['./add-new-address-dialog.component.scss']
})
export class AddNewAddressDialogComponent implements OnInit {
  public data: any;
  public provinces: any[] = [];
  public provinceFilter: any[] = [];
  public districts: any[] = [];
  public homelets: any[] = [];
  public name: string = '';
  public phone: string = '';
  public detailAddress: string = '';
  public homeletId: string = '';
  public isDefault: boolean = false;
  public type: string = '';
  constructor(
    @Inject(MAT_DIALOG_DATA) data: any,
    private loader: LoadingService,
    private notification: NotificationService,
    private userService: UserService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userInfo: UserInfoService,
    notify: NotificationService,
    private dialogService: OpenDialogService,
    private dialogRef: MatDialogRef<AddNewAddressDialogComponent>
  ) {
    this.data = data;
  }
  closeDialog() {
    this.dialogRef.close();
  }
  ngOnInit() {
    this.getAllProvince();
  }
  //location
  getAllProvince() {
    this.loader.showProgressBar();
    this.userService.getAllProvince()
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.provinces = response.data;
          this.provinceFilter = Helpers.clonDeep(this.provinces);
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message! || 'Error when execute!')
        }
      });
  }
  getAllDistrict(provinceId: string) {
    this.loader.showProgressBar();
    this.userService.getDistrictByProvinceId(provinceId)
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.districts = response.data;
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message! || 'Error when execute!')
        }
      });
  }
  getAllHomelet(districtId: string) {
    this.loader.showProgressBar();
    this.userService.getHomeletByDistrictId(districtId)
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.homelets = response.data;
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message! || 'Error when execute!')
        }
      });
  }
  getHomeletByName(name: string) {
    this.loader.showProgressBar();
    this.userService.getHomeletByName(name)
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.homelets = response.data;
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message! || 'Error when execute!')
        }
      });
  }
  getDistrictByName(name: string) {
    this.loader.showProgressBar();
    this.userService.getDistrictByName(name)
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.districts = response.data;
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message! || 'Error when execute!')
        }
      });
  }
  getProvinceByName(name: string) {
    this.loader.showProgressBar();
    this.userService.getProvinceByName(name)
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.provinces = response.data;
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message! || 'Error when execute!')
        }
      });
  }
  changeProvince(event: any) {
    let id = event._value;
    this.homelets = [];
    this.homeletId = '';
    this.getAllDistrict(id.toString());
  }
  changeDistrict(event: any) {
    this.homeletId = '';
    let id = event._value;
    this.getAllHomelet(id.toString());
  }
  changeHomelet(event: any) {
    console.log(event);
    //this.homeletId = event._value.id;
    console.log(this.name, this.phone, this.detailAddress, this.homeletId, this.type, this.isDefault);
  }
  onSearchProvince(event: any) {
    if (event) {
      this.provinceFilter = Helpers.clonDeep(this.provinces.filter(
        p => p.name.includes(event._value.toString())
      ));
    }
  }
  onSelectAddress(event: any) {
    this.type = event;
  }
  formValid() {
    return this.name.length > 0 && this.phone.length > 0
      && this.detailAddress.length > 0 && this.homeletId.length > 0
      && this.type.length > 0
  }
  createAddress() {
    this.loader.showProgressBar();
    this.userService.createAddress(
      {
        id: '',
        homeletAddress: '',
        userId: '',
        detailAddress: this.detailAddress,
        type: this.type,
        isDefault: this.isDefault,
        homeletId: this.homeletId
      })
      .pipe(finalize(() => {
        this.loader.hideProgressBar();
        setTimeout(() => {
          this.closeDialog();
        }, 100);
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notification.notifySuccess('Thêm địa chỉ thành công!');
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message || 'Failed');
        }
      });
  }
  updateAddress() {
    this.userService.createAddress(
      {
        id: '',
        homeletAddress: '',
        userId: '',
        detailAddress: this.detailAddress,
        type: this.type,
        isDefault: this.isDefault,
        homeletId: this.homeletId
      })
      .pipe(finalize(() => {
        this.loader.hideProgressBar();
        setTimeout(() => {
          this.closeDialog();
        }, 100);
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notification.notifySuccess('Thêm địa chỉ thành công!');
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notification.notifyError(response.message || 'Failed');
        }
      });
  }
  onSubmit(event: any) {
    if (!this.formValid()) return;
    this.loader.showProgressBar()
    if (this.data.type === 'add') {
      this.createAddress();
    }
    else if (this.data.type === 'edit') {
      this.updateAddress();
    }
    //this.closeDialog();
  }
}
