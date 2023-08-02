import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginModel } from '../../user.model';
import { NotificationService } from 'src/app/core/service/notification.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { UserService } from '../../user.service';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {
  form: FormGroup;
  constructor(
    private loading: LoadingService,
    private notification: NotificationService,
    private userService: UserService
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
      .pipe(finalize(() => { this.loading.hideProgressBar() }))
      .subscribe((response) => {
        console.log(response);
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          console.log('login success');
          return;
        }
        this.notification.notifyError(response.message || 'Login failed!');
      })
    this.loading.showProgressBar();
    console.log(this.form.getRawValue());
  }
}
