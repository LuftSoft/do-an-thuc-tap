import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { UserService } from '../../user/user.service';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.scss']
})
export class AdminLoginComponent implements OnInit {
  form: FormGroup;
  isShowPassword: boolean = false;
  constructor(
    private loading: LoadingService,
    private notification: NotificationService,
    private userService: UserService,
    private router: Router,
    private userInfoService: UserInfoService,
    private activateRouter: ActivatedRoute,
    private adminService: AdminService
  ) {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  ngOnInit(): void {
  }
  roles = ['3s6xcvcv-2138-4f88-93d8-4d1a5562d047', '4a69b9b9-2138-4f88-93d8-4d1a5562d047'];
  onSubmit() {
    if (!this.form.valid) return;
    this.loading.showProgressBar();
    this.userService.userLogin(this.form.getRawValue())
      .pipe(finalize(() => {
        this.loading.hideProgressBar();
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          localStorage.setItem(CONFIG.AUTH.ADMIN_ACCESS_TOKEN, response.data.accessToken);
          localStorage.setItem(CONFIG.AUTH.ADMIN_REFRESH_TOKEN, response.data.refreshToken);
          this.adminService.getUserInfo().subscribe((response: any) => {
            if (response) {
              let roles = response.role as Array<string>;
              if (roles.includes(CONFIG.ROLE.STAFF) || roles.includes(CONFIG.ROLE.ADMIN)) {
                this.notification.notifySuccess("Đăng nhập thành công!");
                localStorage.setItem(CONFIG.AUTH.ADMIN, JSON.stringify(response));
                setTimeout(() => {
                  this.router.navigate(['/admin'])
                    .then(() => {
                      window.location.reload();
                    });
                }, 100);
              } else {
                this.notification.notifyError("Đăng nhập thất bại!");
                localStorage.removeItem(CONFIG.AUTH.ADMIN);
                localStorage.removeItem(CONFIG.AUTH.ADMIN_ACCESS_TOKEN);
                localStorage.removeItem(CONFIG.AUTH.ADMIN_REFRESH_TOKEN);
              }

            }
          })
        }
        else {
          this.notification.notifyError(response.message || 'Login failed!');
        }
      })
    this.loading.showProgressBar();
  }
  togglePassword(e: any) {
    this.isShowPassword = !this.isShowPassword;
    this.isShowPassword ? e.type = "text" : e.type = "password";
  }
}
