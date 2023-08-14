import { AfterViewChecked, AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { UserService } from '../../user.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { finalize } from 'rxjs';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { ConfirmDialogComponent } from 'src/app/modules/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-shop-cart',
  templateUrl: './shop-cart.component.html',
  styleUrls: ['./shop-cart.component.scss']
})
export class ShopCartComponent implements OnInit, OnDestroy {
  user: any;
  cart: any[] = [];
  selectedCart: any[] = [];
  total_price: any = 0;
  constructor(
    private loader: LoadingService,
    private notification: NotificationService,
    private userService: UserService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userInfo: UserInfoService,
    notify: NotificationService,
    private dialogService: OpenDialogService
  ) { }

  ngOnInit(): void {
    this.activateRoute.params.subscribe(() => {
      this.total_price = 0;
      this.selectedCart = this.userInfo.order;
      this.upDatePrice(this.selectedCart);
      this.getUserCart();
    })
  }
  getUserCart() {
    this.loader.showProgressBar();
    this.userService.getUserCart()
      .pipe(finalize(() => { this.loader.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.cart = response.data;
          console.log(this.cart);
        }
      });
  }
  redirectToProductList() {
    this.router.navigateByUrl('/product');
  }
  getProduct(id: string) {
    var returnResult = this.cart.find(item => item.id == id);
    return returnResult;
  }
  upDatePrice(cart: any[]) {
    this.total_price = 0;
    for (let item of cart) {
      this.total_price += item.phone.soldPrice * item.quantity;
    }
  }
  inputChange(event: any) {
    let productId = event.target.value;
    if (event.target.checked) {
      this.selectedCart.push(this.getProduct(productId));
    }
    else {
      this.selectedCart = this.selectedCart.filter(item => item.id != productId);
    }
    this.upDatePrice(this.selectedCart);
  }
  isBtnDisabled() {
    return this.total_price === 0;
  }
  redirectToOrder() {
    this.userInfo.order = this.selectedCart;
    this.router.navigateByUrl('/payment');
  }
  ngOnDestroy(): void {
    //    this.loader.hideProgressBar();
  }
  isSelectedInCart(id: any) {
    let selectedElement = this.selectedCart.filter(item => item.id === id);
    if (selectedElement) {
      return selectedElement.length > 0;
    }
    return false;
  }
  updateUserCart(cartId: number, phoneId: string, quantity: number, isDelete: boolean) {
    this.loader.showProgressBar();
    this.userService.addToCartByNumber(cartId, phoneId, quantity)
      .pipe(finalize(() => {
        this.loader.hideProgressBar();
        setTimeout(() => {
          this.ngOnInit();
        }, 100);
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          isDelete ? this.notification.notifySuccess("Xóa sản phẩm khỏi giỏ hàng thành công!") :
            this.notification.notifySuccess("Cập nhật số lượng sản phẩm thành công!")
        }
      })
  }
  deleteUserCart(cartId: number) {
    this.loader.showProgressBar();
    this.userService.deleteCart(cartId)
      .pipe(finalize(() => {
        this.loader.hideProgressBar();
        setTimeout(() => {
          this.ngOnInit();
        }, 100);
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notification.notifySuccess("Xóa sản phẩm khỏi giỏ hàng thành công!");
        }
      })
  }
  plusProductQuantity(cartItem: any) {
    if (cartItem.quantity == CONFIG.MAX_PRODUCT) {
      this.dialogService.openDialog(ConfirmDialogComponent, {
        content: `
      <div>Số lượng sản phẩm đã đạt đến mức tối đa</div>
      <div>Quý khách có nhu cầu mua số lượng nhiều vui lòng liên hệ phòng bán hàng:</div>
      <div>Mr.Ngọc: <a style="color:rgb(0, 96, 214);">0788.888.162</a></div>
      <div>Email: <a style="color:rgb(0, 96, 214);">buitatanngoc@gmail.com</a></div>
      `}, '50vw', '30vh')
        .afterClosed().subscribe((response) => { });
      return;
    }
    this.updateUserCart(cartItem.id, cartItem.phone.id, 1, false);
  }
  minusProductQuantity(cartItem: any) {
    if (cartItem.quantity == 1) {
      this.dialogService.openDialog(ConfirmDialogComponent, {
        content: `
      <div>Xóa sản phẩm khỏi giỏ hàng!</div>
      `}, '50vw', '30vh')
        .afterClosed().subscribe((response) => {
          if (response) {
            this.deleteUserCart(cartItem.id);
          }
        });
      return;
    }
    this.updateUserCart(cartItem.id, cartItem.phone.id, - 1, false);
  }
  onDelete(cartItem: any) {
    this.dialogService.openDialog(ConfirmDialogComponent, {
      content: `
    <div>Xóa sản phẩm khỏi giỏ hàng!</div>
    `}, '50vw', '30vh')
      .afterClosed().subscribe((response) => {
        if (response) {
          this.deleteUserCart(cartItem.id);
        }
      });
    return;
  }
}
