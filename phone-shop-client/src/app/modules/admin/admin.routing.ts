import { RouterModule, Routes } from '@angular/router';
import { UserLoginComponent } from '../user/page/user-login/user-login.component';
import { StaffManageComponent } from './staff-manage/staff-manage.component';
import { ReportComponent } from './report/report.component';
import { ProductComponent } from './product/product.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { PromotionComponent } from './promotion/promotion.component';
import { OrderManageComponent } from './order-manage/order-manage.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserInformationComponent } from './user-information/user-information.component';

const routes: Routes = [
    { path: '', component: DashboardComponent },
    { path: 'login', component: UserLoginComponent },
    { path: 'staff', component: StaffManageComponent },
    { path: 'report', component: ReportComponent },
    { path: 'product', component: ProductComponent },
    { path: 'warehouse', component: WarehouseComponent },
    { path: 'promotion', component: PromotionComponent },
    { path: 'order', component: OrderManageComponent },
    { path: 'user-information', component: UserInformationComponent },
    {
        path: '**',
        redirectTo: '/admin/login'
    }
];

export const AdmminRouteModule = RouterModule.forChild(routes);
