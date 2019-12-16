import { Component, OnInit, Inject } from '@angular/core';
import { SubscriptionResult } from '../../models/SubscriptionResult';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SubscriptionService } from '../../services/subscription.service';

@Component({
  selector: 'app-subscription-configuration-popup',
  templateUrl: './subscription-configuration-popup.html',
  styleUrls: ['./subscription-configuration-popup.css']
})
export class SubscriptionConfigurationPopupComponent implements OnInit {

  subscription: SubscriptionResult;
  constructor(private _formBuilder: FormBuilder, private snackbar: MatSnackBar, private subscriptionService: SubscriptionService, private dialogRef: MatDialogRef<SubscriptionConfigurationPopupComponent>, @Inject(MAT_DIALOG_DATA) public data) { }
  messageForm: FormGroup;
  ngOnInit() {
    this.subscription = this.data["subscription"];
    debugger;
    this.messageForm = this._formBuilder.group({
      messagectrl: ['', Validators.required]
    });
  }
  DoRejectSubscription() {
    if (this.messageForm.valid) {
      debugger;
      this.subscriptionService.ConfigureSubscription(this.subscription.id, this.messageForm.get("messagectrl").value
      ).subscribe(
        () => {

          this.dialogRef.close(true);
          this.snackbar.open("Subscription configured successfully", "Ok", { duration: 3000 })
        })
    }
  }
  onclose() {
    this.dialogRef.close(false);
  }
}
