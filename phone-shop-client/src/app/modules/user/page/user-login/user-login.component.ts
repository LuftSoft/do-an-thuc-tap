import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { UserInfoService } from 'src/app/core/service/user.info.service';
import { UserService } from '../../user.service';
import { ShopNavComponent } from '../shop-nav/shop-nav.component';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {
  form: FormGroup;
  isShowPassword: boolean = false;
  constructor(
    private loading: LoadingService,
    private notification: NotificationService,
    private userService: UserService,
    private router: Router,
    private userInfoService: UserInfoService
  ) {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  ngOnInit(): void {
  }
  onSubmit() {
    if (!this.form.valid) return;
    this.loading.showProgressBar();
    this.userService.userLogin(this.form.getRawValue())
      .pipe(finalize(() => {
        this.loading.hideProgressBar();
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          localStorage.setItem('user_access_token', response.data.accessToken);
          localStorage.setItem('user_refresh_token', response.data.refreshToken);
          this.userService.getUserInfo().subscribe((response: any) => {
            if (response) {
              this.notification.notifySuccess("Đăng nhập thành công!");
              localStorage.setItem(CONFIG.AUTH.USER, JSON.stringify(response));
              setTimeout(() => {
                this.router.navigate(['/'])
                  .then(() => {
                    window.location.reload();
                  });
              }, 100);
            }
          })
        } else {
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
