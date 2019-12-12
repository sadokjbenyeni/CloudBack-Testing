import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-confirmation-popup',
  templateUrl: './confirmation-popup.component.html',
  styleUrls: ['./confirmation-popup.component.css']
})
export class ConfirmationPopupComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<ConfirmationPopupComponent>, @Inject(MAT_DIALOG_DATA) public data) { }
  message: string;
  title: string

  ngOnInit() {
    this.message = this.data["message"];
    this.title = this.data["title"];

  }
  onYes() {
    this.dialogRef.close(true)
  }
  onNo() {
    this.dialogRef.close(false)
  }

}
