import { Component, OnInit, Inject } from '@angular/core';
import { UserService } from '../../user/user.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LoadingService } from 'src/app/core/service/loading.service';
import { finalize } from 'rxjs'

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.scss']
})
export class UserInformationComponent implements OnInit {
  name = '';
  user: any = {};
  constructor(
    private load: LoadingService,
    private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.getUserDetail(this.data.id);
  }
  getUserDetail(id: string) {
    this.load.showProgressBar()
    this.userService.getUserDetail(id)
      .pipe(finalize(() => { this.load.hideProgressBar() }))
      .subscribe(data => {
        console.log(data)
        this.user = data;
      })
  }
}
