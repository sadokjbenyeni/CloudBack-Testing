import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpHeaders, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler,): Observable<HttpEvent<any>> {
    let user = localStorage.getItem("user");
    if (user != null) {
      let token = JSON.parse(user)["token"];
      var req = req.clone({
        setHeaders: {
          Authorization: `${token}`
        }
      });
    }
    return next.handle(req);
  }
}
