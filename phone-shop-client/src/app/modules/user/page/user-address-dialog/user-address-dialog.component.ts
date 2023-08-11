import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-address-dialog',
  templateUrl: './user-address-dialog.component.html',
  styleUrls: ['./user-address-dialog.component.scss']
})
export class UserAddressDialogComponent implements OnInit {
  isAddNewAddress: boolean = false;
  constructor() { }

  ngOnInit() {
  }
  addNewAddress() {

  }
  openAddNewAddress() {
    this.isAddNewAddress = !this.isAddNewAddress;
  }
  onEdit() {

  }
  onDelete() {

  }
  onDefault() {

  }
}
