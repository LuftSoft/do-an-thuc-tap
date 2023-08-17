import { Component, OnInit } from '@angular/core';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { UserService } from '../../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { AddNewAddressDialogComponent } from '../add-new-address-dialog/add-new-address-dialog.component';
import { finalize } from 'rxjs';
@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  user: any;
  firstName = '';
  lastName = '';
  phoneNumber = '';
  age = 0;
  constructor(
    private loader: LoadingService,
    private notification: NotificationService,
    private userService: UserService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userInfo: UserInfoService,
    notify: NotificationService,
    private dialogService: OpenDialogService
  ) { }

  ngOnInit(): void {
    this.getUserInfo();
  }
  getUserInfo() {
    this.loader.showProgressBar();
    this.userService.getUserInfo()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response: any) => {
        this.user = response;
        this.firstName = this.user.firstName ? this.user.firstName : '';
        this.lastName = this.user.lastName ? this.user.lastName : '';
        this.phoneNumber = this.user.phoneNumber ? this.user.phoneNumber : '';
        this.age = this.user.phoneNumber ? this.user.age : 0;
      });
  }
  openAddAddressDialog() {
    this.dialogService.openDialog(AddNewAddressDialogComponent, {
      type: 'add'
    },
      '35vw', '72vh')
      .afterClosed().subscribe(data => {
        this.ngOnInit();
      });
  }
  onSetDefault(e: any) {
    this.loader.showProgressBar()
    this.userService.setDefaultAddress(e)
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe(response => {
        if (response) this.notification.notifySuccess('Cập nhật thành công!');
        this.ngOnInit();
      })
  }
  onEditAddress() {

  }
  onDeleteAddress() {

  }
  onUpdateUser() {
    this.loader.showProgressBar()
    this.userService.updateUser({
      Id: this.user.id,
      Email: this.user.email,
      FirstName: this.firstName,
      LastName: this.lastName,
      Age: this.age,
      PhoneNumber: this.phoneNumber,
      Avatar: null
    })
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe(response => {
        if (response) this.notification.notifySuccess('Cập nhật thành công!');
        this.ngOnInit();
      })
  }

}
