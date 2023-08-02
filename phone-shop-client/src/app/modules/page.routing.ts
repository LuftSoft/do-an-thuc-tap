import { Routes, RouterModule } from '@angular/router';
import { ShopLayoutComponent } from './user/page/shop-layout/shop-layout.component';

const routes: Routes = [
  { path: '', loadChildren: () => import('./user/user.module').then(m => m.UserModule) },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },
  { path: 'admin/**', redirectTo: 'admin/login' },
  { path: '**', redirectTo: 'login' }
];

export const PageRoutes = RouterModule.forChild(routes);
