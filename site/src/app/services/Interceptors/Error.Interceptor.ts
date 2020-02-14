import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpHeaders, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, TimeoutError } from 'rxjs';
import { MatDialog } from '@angular/material';
import { ErrorHandlerComponent } from '../../components/error-handler/error-handler.component';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private dialog: MatDialog) { }
  
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).do(event => { }, exception => {
      if (exception instanceof HttpErrorResponse) {
        if (exception.status == 500) {
          this.dialog.open(ErrorHandlerComponent)
        }
      }
      if (exception instanceof TimeoutError) {
        this.dialog.open(ErrorHandlerComponent)
      }
    });
  }
}