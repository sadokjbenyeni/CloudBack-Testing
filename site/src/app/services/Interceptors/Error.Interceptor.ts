import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpHeaders, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, TimeoutError } from 'rxjs';
import { MatDialog } from '@angular/material';
import { ErrorHandlerComponent } from '../../components/error-handler/error-handler.component';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {
  IsPopupOpened: Boolean;
  constructor(private dialog: MatDialog) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(tap(() => { }, exception => {
      if ((exception instanceof HttpErrorResponse && exception.status == 500) || exception instanceof TimeoutError) {
        if (!this.IsPopupOpened)
          this.IsPopupOpened = true;
        this.dialog.open(ErrorHandlerComponent).afterClosed().subscribe(() => {
          this.IsPopupOpened = false;
        })
      }
    }));
  }
}