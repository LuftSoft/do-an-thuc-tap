import { AfterViewChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { Helpers, NotificationService } from 'src/app/core/service/notification.service';
import { AdminService } from 'src/app/modules/admin/admin.service';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-shop-home',
  templateUrl: './shop-home.component.html',
  styleUrls: ['./shop-home.component.scss']
})
export class ShopHomeComponent implements OnInit, AfterViewChecked {
  phones: any[] = [];
  phones_top_4: any[] = [];
  new_phones: any[] = [];
  brands: any[] = [];
  brands_one: any[] = [];
  brands_two: any[] = [];
  news = [
    "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/08/phan-mem-diet-virus-cho-iphone-bg.jpg",
    "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/07/cach-ghep-nhac-vao-anh-1.jpg",
    "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/07/OPPO-A58-NBTC-cover.jpeg",
    "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/07/galaxy-tab-s9-mau-xanh-1.jpeg"
  ]
  constructor(
    private router: Router,
    notify: NotificationService,
    private loader: LoadingService,
    private userService: UserService,
    private adminService: AdminService,
    private activateRoute: ActivatedRoute,
    private dialogService: OpenDialogService,
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.getAllProduct();
    this.getAllbrand();
  }
  getAllProduct() {
    this.loader.showProgressBar();
    this.userService.getListPhone()
      .pipe(finalize(() => this.loader.hideProgressBar()))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.phones = response.data;
          let random = Helpers.random(0, this.phones.length - 5);
          this.phones_top_4 = this.phones.splice(random, 4);
          this.new_phones = this.phones.splice(Helpers.random(0, this.phones.length - 5), 4);
          console.log(this.phones);
        }
      });
  }
  getAllbrand() {
    this.adminService.getAllBrand()
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.brands = response.data;
          this.brands_one = this.brands.slice(0, 4);
          this.brands_two = this.brands.splice(4, this.brands.length);
          console.log(this.brands, this.brands_one, this.brands_two);
        }
      });
  }
  ngAfterViewChecked(): void {
    this.cd.detectChanges();
  }
}
