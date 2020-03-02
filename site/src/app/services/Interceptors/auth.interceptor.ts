import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
  constructor() { }
  intercept(req: HttpRequest<any>, next: HttpHandler, ): Observable<HttpEvent<any>> {
    if (req.url != environment.api + '/v1/user/logout') {
      let token = localStorage.getItem("token");
      if (token != null) {
        var req = req.clone({
          setHeaders: {
            Authorization: `${token}`
          }
        });
      }
    }
    return next.handle(req);
  }

}
