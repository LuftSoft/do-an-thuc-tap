import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
@Injectable({
  providedIn: 'root',
})
export class OpenDialogService{
    
    constructor(public dialog : MatDialog){}
    openDialog(component : any, data : any, width?: string, height?: string){
        this.dialog.open(component,{
            width: width || '1200px',
            height: height || '700px',
            data : data
        })
    }
}