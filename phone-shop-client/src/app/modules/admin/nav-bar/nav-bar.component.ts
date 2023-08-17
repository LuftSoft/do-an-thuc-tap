import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'navbar-cmp',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  sidebarValue: any = {
    'dashboard': {
      value: 'dashboard',
      isSelected: true
    },
    'product': {
      value: 'product',
      isSelected: false
    },
    'warehouse': {
      value: 'warehouse',
      isSelected: false
    },
    'order': {
      value: 'order',
      isSelected: false
    },
    'staff': {
      value: 'staff',
      isSelected: false
    },
    'promotion': {
      value: 'promotion',
      isSelected: false
    },
    'report': {
      value: 'report',
      isSelected: false
    },
    'user_information': {
      value: 'user-information',
      isSelected: false
    },
  }
  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router
  ) {
    let routerArr = router.url.split('/');
    for (let item in this.sidebarValue) {
      this.sidebarValue[item].isSelected = false;
    }
    if (this.sidebarValue[routerArr[routerArr.length - 1]]) {
      this.sidebarValue[routerArr[routerArr.length - 1]].isSelected = true
    }
    else {
      this.sidebarValue['dashboard'].isSelected = true;
    }

  }

  ngOnInit() {
  }
  onLogout() {

  }
  onSideBarSelect(value: string) {
    for (let item in this.sidebarValue) {
      this.sidebarValue[item].isSelected = false;
    }
    this.sidebarValue[value].isSelected = true;
  }
}
