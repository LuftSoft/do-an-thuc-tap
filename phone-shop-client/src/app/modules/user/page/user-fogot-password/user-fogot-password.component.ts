import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-fogot-password',
  templateUrl: './user-fogot-password.component.html',
  styleUrls: ['./user-fogot-password.component.scss']
})
export class UserFogotPasswordComponent implements OnInit {
  form: FormGroup;
  constructor() {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required])
    });
  }
  onSubmit() {

  }
  ngOnInit(): void {
  }

}
