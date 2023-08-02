import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-signup',
  templateUrl: './user-signup.component.html',
  styleUrls: ['./user-signup.component.scss']
})
export class UserSignupComponent implements OnInit {
  form: FormGroup;
  constructor() {
    this.form = new FormGroup({
      //@gmail.com = 9 ki tu r
      email: new FormControl(null, [Validators.required, Validators.minLength(10), Validators.maxLength(500)]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(50)]),
      firstName: new FormControl(null, [Validators.required, Validators.minLength(1)]),
      lastName: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(10)]),
      avatar: new FormControl(null)
    });
  }

  ngOnInit(): void {
  }
  srcResult = '';
  onFileSelected() {
    const inputNode: any = document.querySelector('#file');

    if (typeof (FileReader) !== 'undefined') {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        this.srcResult = e.target.result;
      };

      reader.readAsArrayBuffer(inputNode.files[0]);
    }
  }
  onSubmit() {

  }
}
