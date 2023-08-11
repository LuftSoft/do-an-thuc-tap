import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NotificationService } from 'src/app/core/service/notification.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { ShopCartComponent } from './page/shop-cart/shop-cart.component';
import { ShopHomeComponent } from './page/shop-home/shop-home.component';
import { ShopPaymentComponent } from './page/shop-payment/shop-payment.component';
import { ShopProductDetailComponent } from './page/shop-product-detail/shop-product-detail.component';
import { ShopProductComponent } from './page/shop-product/shop-product.component';
import { UserInfoComponent } from './page/user-info/user-info.component';
import { UserLoginComponent } from './page/user-login/user-login.component';
import { UserOrderComponent } from './page/user-order/user-order.component';
import { UserSignupComponent } from './page/user-signup/user-signup.component';
import { UserRoutesModule } from './user.routing';
import { UserService } from './user.service';
import { ShopFooterComponent } from './page/shop-footer/shop-footer.component';
import { ShopNavComponent } from './page/shop-nav/shop-nav.component';
import { ShopLayoutComponent } from './page/shop-layout/shop-layout.component';
import { UserFogotPasswordComponent } from './page/user-fogot-password/user-fogot-password.component';
import { UserResetPasswordComponent } from './page/user-reset-password/user-reset-password.component';
import { VNDPipe } from 'src/app/core/service/custom.pipe';
import { UserAddressDialogComponent } from './page/user-address-dialog/user-address-dialog.component';
import { ConfirmPaymentDialogComponent } from './page/confirm-payment-dialog/confirm-payment-dialog.component';
import { PaySuccessDialogComponent } from './page/pay-success-dialog/pay-success-dialog.component';
import { AddNewAddressDialogComponent } from './page/add-new-address-dialog/add-new-address-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    UserRoutesModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  declarations: [
    ShopLayoutComponent,
    ShopNavComponent,
    ShopFooterComponent,
    UserLoginComponent,
    UserSignupComponent,
    UserInfoComponent,
    UserFogotPasswordComponent,
    UserResetPasswordComponent,
    UserOrderComponent,
    ShopHomeComponent,
    ShopCartComponent,
    ShopPaymentComponent,
    ShopProductComponent,
    ShopProductDetailComponent,
    UserAddressDialogComponent,
    ConfirmPaymentDialogComponent,
    PaySuccessDialogComponent,
    AddNewAddressDialogComponent,
    VNDPipe
  ],
  exports: [
  ],
  providers: [
    NotificationService,
    UserService
  ]
})
export class UserModule { }
