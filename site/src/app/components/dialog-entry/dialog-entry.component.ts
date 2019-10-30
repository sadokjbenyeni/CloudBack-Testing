import { LoginComponent } from '../login/login.component';
import { Component } from '@angular/core';
import {  MatDialog } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dialog-entry-component',
  template:''
})
export class DialogEntryComponent {

  constructor(
    public dialog: MatDialog,
    private router: Router,
    private route: ActivatedRoute) {
    this.openDialog();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(LoginComponent, { data: {source: this.route.parent.url}
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result==='success')
      {

      this.router.navigate(['../'], { relativeTo: this.route });        
    }
    else{
      this.router.navigateByUrl('home');
    }
    });
  }
}
