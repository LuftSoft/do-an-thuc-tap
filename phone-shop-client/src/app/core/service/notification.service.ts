import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
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

}
