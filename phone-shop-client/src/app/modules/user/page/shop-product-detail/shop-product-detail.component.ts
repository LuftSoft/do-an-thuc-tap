import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  finalize,
  interval,
  Observable,
  startWith,
  Subject,
  switchMap,
  timer,
} from 'rxjs';
import { UserService } from '../../user.service';
import { response } from 'express';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { ConfirmDialogComponent } from 'src/app/modules/confirm-dialog/confirm-dialog.component';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
interface SlideInterface {
  url: string;
  title: string;
}
@Component({
  selector: 'app-shop-product-detail',
  templateUrl: './shop-product-detail.component.html',
  styleUrls: ['./shop-product-detail.component.scss']
})
export class ShopProductDetailComponent implements OnInit, OnDestroy {
  productDetail: any = {};
  productList: any = [];
  cart: any[] = [];
  productCount: number = 1;
  constructor(
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userSerivce: UserService,
    private dialog: OpenDialogService,
    private load: LoadingService,
    private notity: NotificationService
  ) {
    activateRoute.params.subscribe((val) => {
      this.getProductDetail();
      this.getAllProduct();
      this.getUserCart();
    })
  }

  onPaging() { }

  currentIndex: number = 0;
  timeoutId?: number;

  ngOnInit(): void {
    this.resetTimer();
  }
  ngOnDestroy() {
    window.clearTimeout(this.timeoutId);
  }
  getProductDetail() {
    this.userSerivce.getDetailPhone(this.activateRoute.snapshot.paramMap.get('id')?.toString() || '')
      .subscribe(response => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.productDetail = response.data;
        }
      });
  }
  getAllProduct() {
    this.userSerivce.getListPhone()
      .subscribe(response => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.productList = response.data;
          let random = Math.floor(Math.random() * (this.productList.length - 5));
          this.productList = this.productList.splice(random, 4);
        }
      });
  }
  resetTimer() {
    if (this.timeoutId) {
      window.clearTimeout(this.timeoutId);
    }
    this.timeoutId = window.setTimeout(() => this.goToNext(), 3000);
  }
  getUserCart() {
    this.load.showProgressBar();
    this.userSerivce.getUserCart()
      .pipe(finalize(() => { this.load.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.cart = response.data;
        }
      });
  }
  getProductIncart(id: any) {
    return this.cart.filter(p => p.phone.id == id)[0];
  }
  addToCart(product: any) {
    let productCart = this.getProductIncart(product.id);
    console.log(this.productCount, (productCart && (this.productCount + productCart.quantity)));
    if ((productCart && (this.productCount + productCart.quantity) > 5) || this.productCount > 5) {
      this.dialog.openDialog(ConfirmDialogComponent, {
        content: `
      <div>Số lượng sản phẩm đã đạt đến mức tối đa</div>
      <div>Quý khách có nhu cầu mua số lượng nhiều vui lòng liên hệ phòng bán hàng:</div>
      <div>Mr.Ngọc: <a>0788.888.162</a></div
      <div>Email: <a>buitatanngoc@gmail.com</a></div>
      `,
        accept: 'Xem giỏ hàng',
        reject: 'Thoát'
      }, '50vw', '35vh')
        .afterClosed().subscribe((response) => {
          if (response) {
            this.router.navigateByUrl("/cart")
          }
        });
      return;
    }
    this.load.showProgressBar();
    let cartId = productCart ? productCart.id : 0;
    this.userSerivce.addToCartByNumber(cartId, product.id, this.productCount)
      .pipe(finalize(() => { this.load.hideProgressBar() }))
      .subscribe((response) => {
        this.notity.notifySuccess("Thêm sản phẩm thành công!");
      });
    this.getUserCart();
  }
  buyNow(product: any) {
    let productCart = this.getProductIncart(product.id);
    if ((productCart && (this.productCount + productCart.quantity) > 5) || this.productCount > 5) {
      this.dialog.openDialog(ConfirmDialogComponent, {
        content: `
      <div>Số lượng sản phẩm đã đạt đến mức tối đa</div>
      <div>Quý khách có nhu cầu mua số lượng nhiều vui lòng liên hệ phòng bán hàng:</div>
      <div>Mr.Ngọc: <a >0788.888.162</a></div>
      <div>Email: <a >buitatanngoc@gmail.com</a></div>
      `,
        accept: 'Xem giỏ hàng',
        reject: 'Thoát'
      }, '50vw', '35vh')
        .afterClosed().subscribe((response) => {
          if (response) {
            this.router.navigateByUrl("/cart")
          }
        });
      return;
    }
    this.load.showProgressBar();
    let cartId = productCart ? productCart.id : 0;
    this.userSerivce.addToCartByNumber(cartId, product.id, this.productCount)
      .pipe(finalize(() => {
        this.load.hideProgressBar();
        this.router.navigateByUrl('/cart')
      }))
      .subscribe((response) => { });
  }
  goToPrevious(): void {
    const isFirstSlide = this.currentIndex === 0;
    const newIndex = isFirstSlide
      ? this.productDetail.phoneImages.length - 1
      : this.currentIndex - 1;

    this.resetTimer();
    this.currentIndex = newIndex;
  }

  goToNext(): void {
    const isLastSlide = this.currentIndex === this.productDetail.phoneImages.length - 1;
    const newIndex = isLastSlide ? 0 : this.currentIndex + 1;

    this.resetTimer();
    this.currentIndex = newIndex;
  }

  goToSlide(slideIndex: number): void {
    this.resetTimer();
    this.currentIndex = slideIndex;
  }

  getCurrentSlideUrl() {
    if (this.productDetail.phoneImages) {
      return `url('${this.productDetail.phoneImages[this.currentIndex].link}')`;
    }
    return `url('')`;
  }
  onDetail(id: string) {
    this.router.navigateByUrl(`/product/${id}`);
    this.ngOnInit();
  }
  plusProductQuantity() {
    if (this.productCount < (this.productDetail.quantity ? this.productDetail.quantity : 1)) {
      this.productCount++;
    }
  }
  minusProductQuantity() {
    if (this.productCount > 1) {
      this.productCount--;
    }
  }
}