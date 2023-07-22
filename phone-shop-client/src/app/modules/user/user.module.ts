import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserLoginComponent } from './page/user-login/user-login.component';
import { ShopNavComponent } from './page/shop-nav/shop-nav.component';
import { ShopFooterComponent } from './page/shop-footer/shop-footer.component';
import { ShopHomeComponent } from './page/shop-home/shop-home.component';
import { ShopCartComponent } from './page/shop-cart/shop-cart.component';
import { ShopPaymentComponent } from './page/shop-payment/shop-payment.component';
import { UserInfoComponent } from './page/user-info/user-info.component';
import { UserOrderComponent } from './page/user-order/user-order.component';
import { ShopProductComponent } from './page/shop-product/shop-product.component';
import { ShopProductDetailComponent } from './page/shop-product-detail/shop-product-detail.component';
import { UserSignupComponent } from './page/user-signup/user-signup.component';
import { NotificationService } from 'src/app/core/service/notification.service';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [
    UserLoginComponent,
    UserSignupComponent,
    UserInfoComponent,
    UserOrderComponent,
    ShopNavComponent,
    ShopFooterComponent,
    ShopHomeComponent,
    ShopCartComponent,
    ShopPaymentComponent,
    ShopProductComponent,
    ShopProductDetailComponent
  ],
  providers: [
    NotificationService
  ]
})
export class UserModule { }
