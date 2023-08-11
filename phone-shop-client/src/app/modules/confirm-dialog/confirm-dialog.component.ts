import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {
  public data: any;
  constructor(@Inject(MAT_DIALOG_DATA) data: any) {
    this.data = data
  }

  ngOnInit() {
  }
  accept() { }
  reject() { }
}
