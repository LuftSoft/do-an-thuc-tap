import { Component, OnInit } from '@angular/core';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { UserService } from '../../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { AddNewAddressDialogComponent } from '../add-new-address-dialog/add-new-address-dialog.component';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  user: any;
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
    this.user = this.userService.getUserInfo()
      .subscribe((response: any) => {
        this.user = response;
      });
  }
  openAddAddressDialog() {
    this.dialogService.openDialog(AddNewAddressDialogComponent, {
      type: 'add'
    },
      '35vw', '72vh')
      .afterClosed().subscribe(data => {

      });
  }
  onSetDefault() {

  }
  onEditAddress() {

  }
  onDeleteAddress() {

  }
  onUpdateUser() {

  }

}
