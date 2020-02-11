import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpHeaders, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler,): Observable<HttpEvent<any>> {
    return next.handle(req).do(event=>{},exception=>
      {
        debugger;
        if (exception instanceof HttpErrorResponse)
        {
          if (exception.status==500)
          {
            

          }
        }
      });
  }
}
