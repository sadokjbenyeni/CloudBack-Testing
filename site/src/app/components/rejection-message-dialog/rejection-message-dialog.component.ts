import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { SubscriptionService } from '../../services/subscription.service';
import { Subscription } from '../../models/Subscription';
import { debug } from 'util';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SubscriptionResult } from '../../models/SubscriptionResult';

@Component({
  selector: 'app-rejection-message-dialog',
  templateUrl: './rejection-message-dialog.component.html',
  styleUrls: ['./rejection-message-dialog.component.css']
})
export class RejectionMessageDialogComponent implements OnInit {
  subscription: SubscriptionResult;
  constructor(private _formBuilder: FormBuilder, private snackbar: MatSnackBar, private subscriptionService: SubscriptionService, private dialogRef: MatDialogRef<RejectionMessageDialogComponent>, @Inject(MAT_DIALOG_DATA) public data) { }
  messageForm: FormGroup;
  ngOnInit() {
    this.subscription = this.data["subscription"];
    this.messageForm = this._formBuilder.group({
      messagectrl: ['', Validators.required]
    });
  }
  DoRejectSubscription() {
    if (this.messageForm.valid) {
      this.subscriptionService.RejectSubscription(this.subscription.id, this.messageForm.get("messagectrl").value
      ).subscribe(
        () => {

          this.dialogRef.close(true);
          this.snackbar.open("Subscription declined successfully", "Ok", { duration: 3000 })
        })
    }
  }
  onclose() {
    this.dialogRef.close(false);
  }
}
