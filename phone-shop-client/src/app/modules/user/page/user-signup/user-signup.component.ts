import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../user.service';
import { OpenDialogService } from 'src/app/core/service/dialog/opendialog.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { NotificationService } from 'src/app/core/service/notification.service';
import { finalize } from 'rxjs';
import { CONFIG } from 'src/app/core/constant/CONFIG';

@Component({
  selector: 'app-user-signup',
  templateUrl: './user-signup.component.html',
  styleUrls: ['./user-signup.component.scss']
})
export class UserSignupComponent implements OnInit {
  form: FormGroup;
  isShowPassword = false;
  constructor(
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userSerivce: UserService,
    private dialog: OpenDialogService,
    private load: LoadingService,
    private notity: NotificationService
  ) {
    this.form = new FormGroup({
      //@gmail.com = 9 ki tu r
      email: new FormControl(null, [Validators.required, Validators.minLength(10), Validators.maxLength(500)]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(50)]),
      firstName: new FormControl(null, [Validators.required, Validators.minLength(1)]),
      lastName: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(10)]),
      avatar: new FormControl(null),
      role: new FormControl("CUSTOMER")
    });
  }

  ngOnInit(): void {
  }
  srcResult = '';
  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.form.get('avatar')?.setValue(file);
    }
  }
  onSubmit() {
    let submitData = this.form.getRawValue();
    submitData.role = 'CUSTOMER';
    let formData = new FormData();
    formData.append('email', this.form.get('email')!.value);
    formData.append('password', this.form.get('password')!.value);
    formData.append('firstName', this.form.get('firstName')!.value);
    formData.append('lastName', this.form.get('lastName')!.value);
    formData.append('avatar', this.form.get('avatar')!.value);
    formData.append('role', this.form.get('role')!.value);
    this.load.showProgressBar();
    console.log(submitData, formData);
    this.userSerivce.userSignup(formData)
      .pipe(finalize(() => { this.load.hideProgressBar() }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notity.notifySuccess('Đăng ký thành công!');
          setTimeout(() => {
            this.router.navigateByUrl("/login");
          }, 100);
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notity.notifyError(response.message?.toString() ? response.message?.toString() : 'Đăng ký thất bại');
        }
      });
  }
  togglePassword(e: any) {
    this.isShowPassword = !this.isShowPassword;
    this.isShowPassword ? e.type = "text" : e.type = "password";
  }
}
