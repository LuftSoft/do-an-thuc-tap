import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { NotificationService } from 'src/app/core/service/notification.service';
import { AdminService } from '../../admin.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  form: FormGroup;
  brandList: any = [];
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private adminService: AdminService,
    private notify: NotificationService
  ) {
    if (data.type === CONFIG.FORM.TYPE.ADD) {
      this.form = new FormGroup({
        name: new FormControl(null, Validators.required),
        description: new FormControl(null, Validators.required),
        operation: new FormControl(null, Validators.required),
        cpu: new FormControl(null, Validators.required),
        ram: new FormControl(null, Validators.required),
        rom: new FormControl(null, Validators.required),
        pin: new FormControl(null, Validators.required),
        screenSize: new FormControl(null, Validators.required),
        screenResolution: new FormControl(null, Validators.required),
        frontCamera: new FormControl(null, Validators.required),
        behindCamera: new FormControl(null, Validators.required),
        screenTouch: new FormControl(null, Validators.required),
        otherBenefit: new FormControl(null, Validators.required),
        quantity: new FormControl(0, Validators.required),
        importPrice: new FormControl(null, Validators.required),
        soldPrice: new FormControl(null, Validators.required),
        brandId: new FormControl(null, Validators.required),
        brandName: new FormControl(null),
        slug: new FormControl(null, Validators.required),
        phoneImages: new FormControl(null, Validators.required),
      });
    } else {
      let formData = data.data
      this.form = new FormGroup({
        id: new FormControl(formData.id, Validators.required),
        name: new FormControl(formData.name, Validators.required),
        description: new FormControl(formData.description, Validators.required),
        operation: new FormControl(formData.operation, Validators.required),
        cpu: new FormControl(formData.cpu, Validators.required),
        ram: new FormControl(formData.ram, Validators.required),
        rom: new FormControl(formData.rom, Validators.required),
        pin: new FormControl(formData.pin, Validators.required),
        screenSize: new FormControl(formData.screenSize, Validators.required),
        screenResolution: new FormControl(formData.screenResolution, Validators.required),
        frontCamera: new FormControl(formData.frontCamera, Validators.required),
        behindCamera: new FormControl(formData.behindCamera, Validators.required),
        screenTouch: new FormControl(formData.screenTouch, Validators.required),
        otherBenefit: new FormControl(formData.otherBenefit, Validators.required),
        quantity: new FormControl(formData.quantity, Validators.required),
        importPrice: new FormControl(formData.importPrice, Validators.required),
        soldPrice: new FormControl(formData.soldPrice, Validators.required),
        brandId: new FormControl(formData.brand.id, Validators.required),
        brand: new FormControl(formData.brand, Validators.required),
        slug: new FormControl(formData.slug, Validators.required),
      });
    }
  }

  ngOnInit() {
    this.getAllFilter();
  }
  onUpdate() {
    this.adminService.updateProduct(this.form.getRawValue())
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('Cập nhật sản phẩm thành công!');
        }
      });
  }
  onCreate() {
    let formData: FormData = new FormData();
    let formRawData = this.form.getRawValue();
    console.log(formRawData)
    formData.append('name', this.form.controls['name'].value);
    formData.append('description', this.form.controls['description'].value);
    formData.append('operation', this.form.controls['operation'].value);
    formData.append('cpu', this.form.controls['cpu'].value);
    formData.append('ram', this.form.controls['ram'].value);
    formData.append('rom', this.form.controls['rom'].value);
    formData.append('pin', this.form.controls['pin'].value);
    formData.append('screenSize', this.form.controls['screenSize'].value);
    formData.append('screenResolution', this.form.controls['screenResolution'].value);
    formData.append('frontCamera', this.form.controls['frontCamera'].value);
    formData.append('behindCamera', this.form.controls['behindCamera'].value);
    formData.append('screenTouch', this.form.controls['screenTouch'].value);
    formData.append('otherBenefit', this.form.controls['otherBenefit'].value);
    formData.append('quantity', this.form.controls['quantity'].value);
    formData.append('importPrice', this.form.controls['importPrice'].value);
    formData.append('soldPrice', this.form.controls['soldPrice'].value);
    formData.append('brandId', this.form.controls['brandId'].value);
    formData.append('brandName', this.form.controls['brandName'].value);
    formData.append('slug', this.form.controls['slug'].value);
    formData.append('PhoneImages', this.form.controls['phoneImages'].value);
    console.log(formData);
    this.adminService.createProduct(formData)
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('Tạo sản phẩm mới thành công!');
        }
      });
  }

  getBrandFilter() {
    this.adminService.getAllBrand().subscribe((response) => {
      if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
        this.brandList = response.data;
      }
    })
  }
  getAllFilter() {
    this.getBrandFilter();
  }
  srcResult = '';
  fileStr = '';
  onFileSelected(event: any) {
    var formData = new FormData();
    const selectFile = event.target.files;
    var file: any[] = [];
    for (var i = 0; i < selectFile.length; i++) {
      file.push(selectFile[i]);
      formData.append(selectFile[i].name, selectFile[i]);
    }
    this.form.controls['phoneImages'].setValue(file);
    const inputNode: any = document.querySelector('#file');
    this.fileStr = inputNode.files.length;
    if (typeof (FileReader) !== 'undefined') {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        this.srcResult = e.target.result;
      };

      reader.readAsArrayBuffer(inputNode.files[0]);
    }
  }
}
