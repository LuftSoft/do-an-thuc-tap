import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { NotificationService } from 'src/app/core/service/notification.service';
import { AdminService } from '../admin.service';
import { MatTableDataSource } from '@angular/material/table';
import { LoadingService } from 'src/app/core/service/loading.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { ProductDetailComponent } from '../product/product-detail/product-detail.component';
import { BrandDetailComponent } from '../product/brand-detail/brand-detail.component';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { MatPaginator } from '@angular/material/paginator';
import { finalize } from 'rxjs'
import { ImportWarehouseTicketComponent } from './import-warehouse-ticket/import-warehouse-ticket.component';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.scss']
})
export class WarehouseComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  dataSource: MatTableDataSource<any>;
  pageSizeOption = CONFIG.PAGING_OPTION;
  pageSize = this.pageSizeOption[0];
  pageLength = 100;
  displayedColumns = ['name', 'image', 'brand', 'stock', 'edit'];
  constructor(
    private adminService: AdminService,
    private loader: LoadingService,
    private dialog: OpenDialogService,
    private notify: NotificationService
  ) {
    this.dataSource = new MatTableDataSource<any>();
  }

  ngOnInit() {
    this.loader.showProgressBar();
    this.adminService.getAllProduct()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.dataSource.data = response.data;
          this.pageLength = this.dataSource.data.length;
          this.dataSource.paginator = this.paginator;
        }
      })
  }
  onPaging() { }
  onProductDetail(data: any) {
    this.dialog.openDialog(ProductDetailComponent,
      { type: 'view', data: data, title: 'Chi tiết sản phẩm' }, '75vw', '70vh')
  }
  onBrandDetail(brand: any) {
    this.dialog.openDialog(BrandDetailComponent, { type: 'view' }, '75vw', '70vh')
  }
  onEditProductDetail(data: any) {
    this.dialog.openDialog(ProductDetailComponent,
      { type: 'edit', data: data, title: 'Chỉnh sửa sản phẩm' }, '75vw', '70vh')
      .afterClosed().subscribe(response => {
        if (response) {
          this.ngOnInit();
        }
      })
  }
  onDeleteProductDetail(id: string) {
    this.dialog.openDialog(ConfirmDialogComponent, {}, '300px', '200px')
      .afterClosed().subscribe((response) => {
        if (response) {
          this.adminService.deleteProduct(id).subscribe((response) => {
            if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
              this.notify.notifySuccess('Delete product success');
            }
          });
        }
      })
  }
  onCreateProduct() {
    this.dialog.openDialog(ProductDetailComponent,
      { type: 'add', title: 'Thêm sản phẩm' }, '75vw', '70vh')
      .afterClosed().subscribe(response => {
        if (response) {
          this.ngOnInit();
        }
      })
  }
  onCreateWarehouseTicket() {
    this.dialog.openDialog(ImportWarehouseTicketComponent, {},
      '30vw', '50vh'
    )
      .afterClosed().subscribe(response => {
        console.log(response);
        if (response) {
          this.notify.notifySuccess('Nhap hang thanh cong');
          this.ngOnInit();
        }
      });
  }
}

