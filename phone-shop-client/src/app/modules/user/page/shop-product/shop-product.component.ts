import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../user.service';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Helpers } from 'src/app/core/service/notification.service';
import { AdminService } from 'src/app/modules/admin/admin.service';
import { response } from 'express';

@Component({
  selector: 'app-shop-product',
  templateUrl: './shop-product.component.html',
  styleUrls: ['./shop-product.component.scss']
})
export class ShopProductComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  cart: any = [];
  brandList: any = [];
  pageSize = CONFIG.PAGING_OPTION[0];
  pageOption = CONFIG.PAGING_OPTION;
  datasource = new MatTableDataSource<any>();
  constructor(
    private router: Router,
    private userService: UserService,
    private adminSerivce: AdminService
  ) { }

  ngOnInit(): void {
    this.getAllProduct();
    this.getAllBrand();
  }
  onPaging(p: any) {
    console.log(p)
    this.cart = Helpers.clonDeep(this.datasource.data);
    this.cart = this.cart.splice(p._pageIndex * p._pageSize, p._pageSize);
  }
  getAllProduct() {
    this.userService.getListPhone()
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.cart = Helpers.clonDeep(response.data);
          this.datasource.data = response.data;
          this.datasource.paginator = this.paginator;
          this.cart = this.cart.splice(0, 5);
        }
      })
  }
  getAllBrand() {
    this.adminSerivce.getAllBrand().subscribe((response) => {
      if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
        this.brandList = response.data;
      }
    });
  }
  onDetail(id: string) {
    this.router.navigateByUrl(`/product/${id}`);
  }
  filterByBrand(brandId: any) {

  }
  addToCart(phoneId: string) {

  }
}
