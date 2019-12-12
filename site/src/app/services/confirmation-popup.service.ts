import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Observable } from 'rxjs';
import { ConfirmationPopupComponent } from '../components/confirmation-popup/confirmation-popup.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationPopupService {

  constructor(private dialog: MatDialog) { }
  public openPopup(message: String, title: String): Observable<boolean> {
    return this.dialog.open(ConfirmationPopupComponent, { disableClose: true, data: { message: message, title: title } }).afterClosed()
  }
}
