import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-reset-password',
  templateUrl: './user-reset-password.component.html',
  styleUrls: ['./user-reset-password.component.scss']
})
export class UserResetPasswordComponent implements OnInit {
  form: FormGroup;
  constructor(
    private route: ActivatedRoute
  ) {
    this.form = new FormGroup({
      token: new FormControl(null),
      password: new FormControl(null, [Validators.required]),
      duplicatePassword: new FormControl(null, [Validators.required])
    })
  }
  onSubmit() {
    console.log(this.route.snapshot.paramMap.get('token'));
  }
  ngOnInit(): void {
  }

}
