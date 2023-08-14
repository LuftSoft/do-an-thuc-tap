import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../user.service';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Helpers, NotificationService } from 'src/app/core/service/notification.service';
import { AdminService } from 'src/app/modules/admin/admin.service';
import { debounce, debounceTime, delay, filter, finalize } from 'rxjs';
import { LoadingService } from 'src/app/core/service/loading.service';

@Component({
  selector: 'app-shop-product',
  templateUrl: './shop-product.component.html',
  styleUrls: ['./shop-product.component.scss']
})
export class ShopProductComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  cart: any = [];
  cartFilter: any[] = []
  brandList: any[] = [];
  brandFilterList: any[] = [];
  pageSize = CONFIG.PAGING_OPTION[0];
  pageOption = CONFIG.PAGING_OPTION;
  datasource = new MatTableDataSource<any>();
  priceOptions: any[] = [
    "Giá thấp đến cao",
    "Giá cao đến thấp"
  ];
  ramOptions: any[] = [
    { value: "4GB" },
    { value: "6GB" },
    { value: "8GB" },
    { value: "12GB" }
  ];
  romOptions: any[] = [
    { value: "32GB" },
    { value: "64GB" },
    { value: "128GB" },
    { value: "256GB" },
    { value: "512GB" },
  ];
  hzOptions: any[] = [
    { value: "60Hz" },
    { value: "90Hz" },
    { value: "120Hz" },
  ];
  cpuOptions: any[] = [
    { value: "Snapdragon" },
    { value: "Apple A" },
    { value: "Mediatek Dimensity" },
    { value: "Mediatek Helio" },
    { value: "Exynos" }

  ];
  screenSizeOptions: any[] = [
    { value: "Trên 6 inch" },
    { value: "Dưới 6 inch" }
  ];
  searchKey: string = '';
  brandFilter = [];
  filter = {
    search: '',
    price: '',
    ram: '',
    rom: '',
    cpu: '',
    hz: '',
    size: '',
  }
  constructor(
    private router: Router,
    private userService: UserService,
    private adminSerivce: AdminService,
    private loader: LoadingService,
    private notity: NotificationService
  ) {
    userService.searchEvent
      .pipe(debounceTime(500))
      .subscribe((data) => {
        this.filter.search = data.trim();
        this.filterData();
      });
  }

  ngOnInit(): void {
    this.getAllProduct();
    this.getAllBrand();
  }
  onPaging(p: any) {
    this.cartFilter = Helpers.clonDeep(this.datasource.data);
    this.cartFilter = this.cartFilter.splice(p._pageIndex * p._pageSize, p._pageSize);
  }
  getAllProduct() {
    this.loader.showProgressBar();
    this.userService.getListPhone()
      .pipe(finalize(() => {
        this.loader.hideProgressBar();
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.cart = Helpers.clonDeep(response.data);
          this.cartFilter = Helpers.clonDeep(this.cart);
          this.datasource.data = response.data;
          this.datasource.paginator = this.paginator;
          this.cartFilter = this.cartFilter.slice(0, 5);
        }
      })
  }
  getAllBrand() {
    this.adminSerivce.getAllBrand().subscribe((response) => {
      if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
        this.brandList = response.data;
        this.brandList.forEach(b => b.isSelect = false)
      }
    });
  }
  onDetail(id: string) {
    this.router.navigateByUrl(`/product/${id}`);
  }
  isSelectChange(e: any) {
    return e.value == -1 ? false : true;
  }
  addToCartService(phoneId: any, quantity: number) {
    this.loader.showProgressBar();
    this.userService.addToCart(phoneId, quantity)
      .pipe(finalize(() => { this.loader.hideProgressBar(); }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notity.notifySuccess("Thêm vào giỏ hàng thành công!");
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notity.notifyError(response.message?.toString()!);
        }
      })
  }
  addToCart(phone: any) {
    Helpers.checkUser(this.router);
    this.addToCartService(phone.id, 1);
  }
  onBuyNow(phone: any) {
    Helpers.checkUser(this.router);
    this.loader.showProgressBar();
    this.userService.addToCart(phone.id, 1)
      .pipe(finalize(() => {
        this.loader.hideProgressBar();
        setTimeout(() => {
          this.router.navigateByUrl("/cart");
        }, 100);
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notity.notifySuccess("Thêm vào giỏ hàng thành công!");
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notity.notifyError(response.message?.toString()!);
        }
      })
  }
  filterData() {
    this.datasource.data = Helpers.clonDeep(this.cart);
    for (let item in this.filter) {
      switch (item) {
        case 'search':
          if (this.filter.search !== '') {
            this.datasource.data = this.datasource.data.filter(p => Helpers.removeVietnameseTones(p.name)
              .includes(Helpers.removeVietnameseTones(this.filter.search)))
          }
          break;
        case 'price':
          if (this.filter[item] !== '') {
            switch (this.filter[item]) {
              case '0':
                this.datasource.data = this.datasource.data.sort((a: any, b: any) => (a.soldPrice > b.soldPrice ? 1 : -1));
                break;
              case '1':
                this.datasource.data = this.datasource.data.sort((a: any, b: any) => (a.soldPrice < b.soldPrice ? 1 : -1));
                break;
            }
          }
          break;
        case 'ram':
          if (this.filter[item] !== '') {
            this.datasource.data = this.datasource.data.filter(p => p.ram == this.filter.ram);
          }
          break;
        case 'rom':
          if (this.filter[item] !== '') {
            this.datasource.data = this.datasource.data.filter(p => p.rom == this.filter.rom);
          }
          break;
        case 'cpu':
          if (this.filter[item] !== '') {
            this.datasource.data = this.datasource.data.filter(p => p.cpu == this.filter.cpu);
          }
          break;
        case 'hz':
          if (this.filter[item] !== '') {
            this.datasource.data = this.datasource.data.filter(p => p.screenResolution == this.filter.hz.split('Hz')[0]);
          }
          break;
        case 'size':
          //Tren || Duoi
          if (this.filter[item] !== '') {
            let option = this.filter.size.split(' ');
            option[0] == "Trên" ? this.datasource.data = this.datasource.data.filter(p => p.screenSize > Number(option[1]))
              : this.datasource.data = this.datasource.data.filter(p => p.screenSize < Number(option[1]));
          }
          break;
      }
    }
    if (this.brandFilterList.length > 0) {
      this.datasource.data = this.datasource.data.filter(p => this.brandFilterList.includes(p.brand.id));
    }
    this.cartFilter = this.datasource.data.slice(0, 5);
  }
  changePrice(e: any) {
    e.target.value == -1 ? this.filter.price = '' : this.filter.price = e.target.value.toString();
    this.filterData();
  }
  changeCpu(e: any) {
    e.target.value == -1 ? this.filter.cpu = '' : this.filter.cpu = e.target.value.toString();
    this.filterData();
  }
  changeRam(e: any) {
    e.target.value == -1 ? this.filter.ram = '' : this.filter.ram = e.target.value.toString();
    this.filterData();
  }
  changeRom(e: any) {
    e.target.value == -1 ? this.filter.rom = '' : this.filter.rom = e.target.value.toString();
    this.filterData();
  }
  changeHz(e: any) {
    e.target.value == -1 ? this.filter.hz = '' : this.filter.hz = e.target.value.toString();
    this.filterData();
  }
  changeSize(e: any) {
    e.target.value == -1 ? this.filter.size = '' : this.filter.size = e.target.value.toString();
    this.filterData();
  }
  toggleBrand(id: string) {
    let brand = this.brandList.find(b => b.id == id);
    brand.isSelect = !brand.isSelect;
    if (brand.isSelect) {
      this.brandFilterList.push(brand.id)
    }
    else {
      this.brandFilterList = this.brandFilterList.filter(b => b != brand.id);
    }
    this.filterData();
  }
}
