import { NgModule } from '@angular/core';
import { PageRoutes } from './page.routing';
import { UserModule } from './user/user.module';
import { ShopFooterComponent } from './user/page/shop-footer/shop-footer.component';
import { ShopNavComponent } from './user/page/shop-nav/shop-nav.component';
import { ShopLayoutComponent } from './user/page/shop-layout/shop-layout.component';
import { AppModule } from '../app.module';
import { SharedModule } from '../shared/shared.module';
@NgModule({
    declarations: [],
    imports: [
        PageRoutes,
        UserModule,
    ],
    exports: [
        UserModule,
    ],
    providers: []
})
export class PageModule {

}