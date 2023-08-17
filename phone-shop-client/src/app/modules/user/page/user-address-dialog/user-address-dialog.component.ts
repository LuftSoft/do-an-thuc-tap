import { Component, OnInit, Inject } from '@angular/core';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { UserService } from '../../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { finalize } from 'rxjs';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddNewAddressDialogComponent } from '../add-new-address-dialog/add-new-address-dialog.component';

@Component({
  selector: 'app-user-address-dialog',
  templateUrl: './user-address-dialog.component.html',
  styleUrls: ['./user-address-dialog.component.scss']
})
export class UserAddressDialogComponent implements OnInit {
  isAddNewAddress: boolean = false;
  selectedId: string = '';
  user: any = {};
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
  ) {
    this.selectedId = data.id;
  }

  ngOnInit() {
    this.getUserInfo();
  }
  getUserInfo() {
    this.loader.showProgressBar();
    this.userService.getUserInfo()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response: any) => {
        this.user = response;
      });
  }
  addNewAddress() {

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
  onEdit() {

  }
  onDelete() {

  }
  onDefault(id: any) {
    this.selectedId = id;
  }
}
