import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UserService } from 'src/app/modules/user/user.service';
@Injectable({
  providedIn: 'root',
})

export class NotificationService {

  constructor(private snackbar: MatSnackBar) { }
  openSnackBar(message: string, action: string) {
    this.snackbar.open(message, action, {
      verticalPosition: 'top',
      horizontalPosition: 'center',
      duration: 1000
    });
  }
  notifySuccess(message: string) {
    this.snackbar.open(message, "X", {
      verticalPosition: 'top',
      horizontalPosition: 'center',
      duration: 1000,
      panelClass: 'notify-panel-success'
    });
  }
  notifyError(message: string) {
    this.snackbar.open(message, "X", {
      verticalPosition: 'top',
      horizontalPosition: 'center',
      duration: 1000,
      panelClass: 'notify-panel-error'
    });
  }
  notifyWarning(message: string) {
    this.snackbar.open(message, "X", {
      verticalPosition: 'top',
      horizontalPosition: 'center',
      duration: 1000,
      panelClass: 'notify-panel-warning'
    });
  }
  notifyInfo(message: string) {
    this.snackbar.open(message, "X", {
      verticalPosition: 'top',
      horizontalPosition: 'center',
      duration: 1000,
      panelClass: 'notify-panel-info'
    });
  }

}
export class Helpers {
  static clonDeep(obj: any) {
    return JSON.parse(JSON.stringify(obj));
  }
  static parse(obj: any) {
    return JSON.parse(obj);
  }
  static checkUser(router: Router) {
    const user = localStorage.getItem('user');
    if (!user) {
      router.navigateByUrl("/login");
      return false;
    }
    return true;
  }
  static naviGateToLogin(router: Router) {
    router.navigateByUrl("/login");
  }
  static random(min: number, max: number) {
    return Math.floor(Math.random() * (max - min) + min);
  }
}