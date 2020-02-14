import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-error-handler',
  templateUrl: './error-handler.component.html',
  styleUrls: ['./error-handler.component.css']
})
export class ErrorHandlerComponent implements OnInit {

  constructor( @Inject(MAT_DIALOG_DATA) public data, public dialogRef: MatDialogRef<ErrorHandlerComponent>,) { }
  ngOnInit() {
  }
  onOk(): void {
    this.dialogRef.close();
  }
}
