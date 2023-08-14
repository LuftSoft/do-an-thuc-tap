import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CONFIG } from 'src/app/core/constant/CONFIG';
import { NotificationService } from 'src/app/core/service/notification.service';
import { AdminService } from '../../admin.service';
import { LoadingService } from 'src/app/core/service/loading.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  form: FormGroup;
  brandList: any = [];
  operationOptions = CONFIG.PHONE.OPERATION_OPTIONS
  cpuOptions = CONFIG.PHONE.CPU_OPTIONS;
  ramOptions = CONFIG.PHONE.RAM_OPTIONS;
  romOptions = CONFIG.PHONE.ROM_OPTIONS;
  hzOptions = CONFIG.PHONE.HZ_OPTIONS;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private adminService: AdminService,
    private notify: NotificationService,
    private load: LoadingService,
    private dialogRef: MatDialogRef<ProductDetailComponent>
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
        operation: new FormControl({ value: formData.operation, disabled: data.type === CONFIG.FORM.TYPE.VIEW }, Validators.required),
        cpu: new FormControl({ value: formData.cpu, disabled: data.type === CONFIG.FORM.TYPE.VIEW }, Validators.required),
        ram: new FormControl({ value: formData.ram, disabled: data.type === CONFIG.FORM.TYPE.VIEW }, Validators.required),
        rom: new FormControl({ value: formData.rom, disabled: data.type === CONFIG.FORM.TYPE.VIEW }, Validators.required),
        pin: new FormControl(formData.pin, Validators.required),
        screenSize: new FormControl(formData.screenSize, Validators.required),
        screenResolution: new FormControl({ value: formData.screenResolution, disabled: data.type === CONFIG.FORM.TYPE.VIEW }, Validators.required),
        frontCamera: new FormControl(formData.frontCamera, Validators.required),
        behindCamera: new FormControl(formData.behindCamera, Validators.required),
        screenTouch: new FormControl(formData.screenTouch, Validators.required),
        otherBenefit: new FormControl(formData.otherBenefit, Validators.required),
        quantity: new FormControl(formData.quantity, Validators.required),
        importPrice: new FormControl(formData.importPrice, Validators.required),
        soldPrice: new FormControl(formData.soldPrice, Validators.required),
        brandId: new FormControl({ value: formData.brand.id, disabled: data.type === CONFIG.FORM.TYPE.VIEW }, Validators.required),
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
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notify.notifySuccess('Cập nhật sản phẩm không thành công!');
        }
      });
  }
  onCreate() {
    let formData = new FormData();
    let formRawData = this.form.getRawValue();
    console.log(formRawData)
    formData.append('name', this.form.get('name')!.value);
    formData.append('description', this.form.get('description')!.value);
    formData.append('operation', this.form.get('operation')!.value);
    formData.append('cpu', this.form.get('cpu')!.value);
    formData.append('ram', this.form.get('ram')!.value);
    formData.append('rom', this.form.get('rom')!.value);
    formData.append('pin', this.form.get('pin')!.value);
    formData.append('screenSize', this.form.get('screenSize')!.value);
    formData.append('screenResolution', this.form.get('screenResolution')!.value);
    formData.append('frontCamera', this.form.get('frontCamera')!.value);
    formData.append('behindCamera', this.form.get('behindCamera')!.value);
    formData.append('screenTouch', this.form.get('screenTouch')!.value);
    formData.append('otherBenefit', this.form.get('otherBenefit')!.value);
    formData.append('quantity', this.form.get('quantity')!.value);
    formData.append('importPrice', this.form.get('importPrice')!.value);
    formData.append('soldPrice', this.form.get('soldPrice')!.value);
    formData.append('brandId', this.form.get('brandId')!.value);
    formData.append('brandName', this.form.get('brandName')!.value);
    formData.append('slug', this.form.get('slug')!.value);
    for (let file of this.form.get('phoneImages')!.value) {
      formData.append('phoneImages', file);
    }
    this.load.showProgressBar();
    this.adminService.createProduct(formData)
      .pipe(finalize(() => {
        this.load.hideProgressBar();
        setTimeout(() => {
          this.dialogRef.close(true);
        }, 100);
      }))
      .subscribe((response) => {
        if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
          this.notify.notifySuccess('Tạo sản phẩm mới thành công!');
        }
        else if (response.code === CONFIG.STATUS_CODE.ERROR) {
          this.notify.notifySuccess('Failed!');
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
    const files: File[] = event.target.files;
    this.fileStr = files.length.toString();
    if (files) {
      var tmp: any[] = [];
      for (let f of files) {
        tmp.push(f);
      }
      this.form.get('phoneImages')?.setValue(tmp);
    }


  }
}
