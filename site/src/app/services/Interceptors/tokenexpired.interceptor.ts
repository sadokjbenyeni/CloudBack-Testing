import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from '../user.service';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import * as jwt_decode from 'jwt-decode'
@Injectable({
  providedIn: 'root'
})
export class TokenExpiredInterceptor implements HttpInterceptor {
  constructor(private userService: UserService, private router: Router) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(tap(() => { }
      , error => {
        if (error instanceof HttpErrorResponse) {
          debugger;
          if (error['error'] == 'token expired') {
            let token=jwt_decode(localStorage.getItem('token'))['token']
            this.userService.logout(token).subscribe(
              () => {
                localStorage.clear();
                this.router.navigateByUrl("/login")
              }
            );
          }
        }
      }
    ))
  }
}
