import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
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
interface SlideInterface {
  url: string;
  title: string;
}
@Component({
  selector: 'app-shop-product-detail',
  templateUrl: './shop-product-detail.component.html',
  styleUrls: ['./shop-product-detail.component.scss']
})
export class ShopProductDetailComponent implements OnInit {
  productDetail: any = {};
  productList: any = [];
  productCount: number = 1;
  constructor(
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userSerivce: UserService
  ) {
    activateRoute.params.subscribe((val) => {
      this.getProductDetail();
      this.getAllProduct();
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
    return `url('${this.productDetail.phoneImages[this.currentIndex].link}')`;
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