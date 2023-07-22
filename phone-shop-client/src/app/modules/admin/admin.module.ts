import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService } from 'src/app/core/service/notification.service';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        CommonModule,
        SharedModule
    ],
    declarations: [
    ],
    providers: [
        NotificationService
    ]
})
export class AdminModule { }