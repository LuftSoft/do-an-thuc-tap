import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/core/service/notification.service';

@Component({
  selector: 'app-shop-nav',
  templateUrl: './shop-nav.component.html',
  styleUrls: ['./shop-nav.component.scss']
})
export class ShopNavComponent implements OnInit {

  constructor(private notify: NotificationService) { }

  ngOnInit(): void {
  }
  openNotify() {
    this.notify.openSnackBar("test snack bar", "close");
  }
}
