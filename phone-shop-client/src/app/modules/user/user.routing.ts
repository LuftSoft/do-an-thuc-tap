import { Routes, RouterModule } from '@angular/router';
import { UserLoginComponent } from './page/user-login/user-login.component';
import { ShopHomeComponent } from './page/shop-home/shop-home.component';
import { UserSignupComponent } from './page/user-signup/user-signup.component';
import { UserFogotPasswordComponent } from './page/user-fogot-password/user-fogot-password.component';
import { UserResetPasswordComponent } from './page/user-reset-password/user-reset-password.component';
import { ShopCartComponent } from './page/shop-cart/shop-cart.component';
import { ShopPaymentComponent } from './page/shop-payment/shop-payment.component';
import { ShopProductComponent } from './page/shop-product/shop-product.component';
import { ShopProductDetailComponent } from './page/shop-product-detail/shop-product-detail.component';
import { UserInfoComponent } from './page/user-info/user-info.component';
import { UserOrderComponent } from './page/user-order/user-order.component';
import { ShopLayoutComponent } from './page/shop-layout/shop-layout.component';

const routes: Routes = [
  //auth
  {
    path: '', component: ShopLayoutComponent, children: [
      { path: 'login', component: UserLoginComponent },
      { path: 'signup', component: UserSignupComponent },
      { path: 'forgot-password', component: UserFogotPasswordComponent },
      { path: 'reset-password', component: UserResetPasswordComponent },
      { path: 'info', component: UserInfoComponent },
      { path: 'order', component: UserOrderComponent },
      //shop
      { path: '', component: ShopHomeComponent },
      { path: 'cart', component: ShopCartComponent },
      {
        path: 'product', children: [
          { path: '', component: ShopProductComponent },
          { path: 'detail', component: ShopProductDetailComponent }
        ]
      },
      { path: 'payment', component: ShopPaymentComponent }
    ]
  }
];

export const UserRoutesModule = RouterModule.forChild(routes);
