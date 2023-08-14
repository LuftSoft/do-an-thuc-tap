import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NotificationService } from 'src/app/core/service/notification.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { AdmminRouteModule } from './admin.routing';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { StaffManageComponent } from './staff-manage/staff-manage.component';
import { ProductComponent } from './product/product.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { OrderManageComponent } from './order-manage/order-manage.component';
import { PromotionComponent } from './promotion/promotion.component';
import { ReportComponent } from './report/report.component';
import { AdminService } from './admin.service';
import { ProductDetailComponent } from './product/product-detail/product-detail.component';
import { BrandDetailComponent } from './product/brand-detail/brand-detail.component';
import { ImportWarehouseTicketComponent } from './warehouse/import-warehouse-ticket/import-warehouse-ticket.component';
import { OrderDetailComponent } from './order-manage/order-detail/order-detail.component';
import { AdminVNDPipe, VNDPipe } from 'src/app/core/service/custom.pipe';
import { DashboardComponent } from './dashboard/dashboard.component';
//const NgbModule = require('@ng-bootstrap/ng-bootstrap')

@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        HttpClientModule,
        ReactiveFormsModule,
        AdmminRouteModule,
        RouterModule,
        FormsModule
    ],
    declarations: [
        AdminLayoutComponent,
        NavBarComponent,
        FooterComponent,
        StaffManageComponent,
        ProductComponent,
        WarehouseComponent,
        DashboardComponent,
        OrderManageComponent,
        PromotionComponent,
        ReportComponent,
        ProductDetailComponent,
        BrandDetailComponent,
        ImportWarehouseTicketComponent,
        OrderDetailComponent,
        AdminVNDPipe
    ],
    providers: [
        NotificationService,
        AdminService
    ]
})
export class AdminModule { }