import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoadingServiceComponent } from './core/service/loading-service/loading-service.component';
import { NotificationComponent } from './core/service/notification/notification.component';
import { AdminModule } from './modules/admin/admin.module';
import { PageModule } from './modules/page.module';
import { ShopFooterComponent } from './modules/user/page/shop-footer/shop-footer.component';
import { ShopLayoutComponent } from './modules/user/page/shop-layout/shop-layout.component';
import { ShopNavComponent } from './modules/user/page/shop-nav/shop-nav.component';
import { UserService } from './modules/user/user.service';
import { ConfirmDialogComponent } from './modules/confirm-dialog/confirm-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatPaginatorModule } from '@angular/material/paginator';
import { UserInfoService } from './core/service/user.info.service';
import { VNDPipe } from './core/service/custom.pipe';

@NgModule({
  declarations: [
    AppComponent,
    NotificationComponent,
    LoadingServiceComponent,
    ConfirmDialogComponent,
    // ShopNavComponent,
    // ShopFooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AdminModule,
    PageModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatPaginatorModule
  ],
  providers: [
    UserService,
    UserInfoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
