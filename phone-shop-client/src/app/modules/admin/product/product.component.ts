import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { elementAt, finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { MatTableDataSource } from '@angular/material/table';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { BrandDetailComponent } from './brand-detail/brand-detail.component';
import { NotificationService } from 'src/app/core/service/notification.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  dataSource: MatTableDataSource<any>;
  pageSizeOption = CONFIG.PAGING_OPTION;
  pageSize = this.pageSizeOption[0];
  pageLength = 100;
  displayedColumns = ['name', 'image', 'brand', 'operation', 'edit'];
  constructor(
    private adminService: AdminService,
    private loader: LoadingService,
    private dialog: OpenDialogService,
    private notify: NotificationService
  ) {
    this.dataSource = new MatTableDataSource<any>();
  }

  ngOnInit() {
    //const load = this.loader.showProgressBar();
    this.adminService.getAllProduct()
      //.pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.dataSource.data = response.data;
          this.pageLength = this.dataSource.data.length;
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
}
