import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pay-success-dialog',
  templateUrl: './pay-success-dialog.component.html',
  styleUrls: ['./pay-success-dialog.component.scss']
})
export class PaySuccessDialogComponent implements OnInit {
  public data: any;
  constructor(
    @Inject(MAT_DIALOG_DATA) data: any,
    private router: Router,
    private dialogRef: MatDialogRef<PaySuccessDialogComponent>
  ) {
    this.data = data
  }

  ngOnInit() {
  }
  onClose() {
    this.dialogRef.close();
  }
  buyProduct() {
    this.onClose();
    this.router.navigateByUrl('/product');
  }
  showOrder() {
    this.onClose();
    this.router.navigateByUrl('/order');
  }
  accept() { }
  reject() { }
}
