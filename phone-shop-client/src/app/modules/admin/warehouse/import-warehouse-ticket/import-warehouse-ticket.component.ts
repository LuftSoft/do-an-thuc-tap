import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../admin.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { MatDialogRef } from '@angular/material/dialog';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { finalize } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-import-warehouse-ticket',
  templateUrl: './import-warehouse-ticket.component.html',
  styleUrls: ['./import-warehouse-ticket.component.scss']
})
export class ImportWarehouseTicketComponent implements OnInit {
  suppliers: any[] = [];
  phones: any[] = [];
  selectPhone: any = '';
  phoneCount: number = 0;
  listPhoneAdd: any[] = [];
  form: FormGroup;
  constructor(
    private loader: LoadingService,
    private notify: NotificationService,
    private adminService: AdminService,
    private dialogService: OpenDialogService,
    private dialogRef: MatDialogRef<ImportWarehouseTicketComponent>
  ) {
    this.form = new FormGroup({
      supplierId: new FormControl(null, [Validators.required]),
    });
  }
  onCreate() {
    this.loader.showProgressBar();
    this.adminService.createWarehouseTicket({
      SupplierId: this.form.get('supplierId')?.value,
      UserId: null,
      created: new Date(),
      phones: this.listPhoneAdd
    })
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        this.dialogRef.close(true);
      });
  }
  ngOnInit() {
    this.getAllData();
  }
  getAllData() {
    this.getAllPhone();
    this.getAllSupplier();
  }
  getAllSupplier() {
    this.loader.showProgressBar();
    this.adminService.getAllSupplier()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        this.suppliers = response.data;
      })
  }
  getAllPhone() {
    this.loader.showProgressBar();
    this.adminService.getAllProduct()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.phones = response.data;
        }
        else if (response.code === CONFIG.STATUS_CODE.SUCCESS) {

        }
      })
  }
  changePhone($event: any) {
    if ($event) {
      this.selectPhone = $event.value;
    }
  }
  changeCount(e: any) {
    if (e) {
      this.phoneCount = e.target.value;
    }
  }
  isAddPhone() {
    if (this.selectPhone.length > 0 && this.phoneCount > 0) {
      return true;
    }
    return false;
  }
  addPhone() {
    this.listPhoneAdd.push({
      phoneId: this.selectPhone,
      quantity: this.phoneCount
    });
    this.selectPhone = '';
    this.phoneCount = 0;
  }
  getPhone(id: string) {
    let result = this.phones.filter(p => p.id == id)[0];
    return result;
  }
  deletePhone(id: any) {
    this.listPhoneAdd = this.listPhoneAdd.filter(p => p.phoneId != id);
  }
}
