import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopNavComponent } from './modules/user/page/shop-nav/shop-nav.component';

const routes: Routes = [
  { path: '', component: ShopNavComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
