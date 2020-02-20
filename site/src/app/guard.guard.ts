import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class GuardGuard implements CanActivate {

  constructor(
    public router: Router,
  ) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let user = JSON.parse(localStorage.getItem('user'));

    if (user) {
      if (next.data["roles"] == undefined) {
        return true
      }
      for (let userrole of user["roleName"]) {
        if ((next.data["roles"] as Array<string>).indexOf(userrole) != -1)
          return true;
      }
      this.router.navigate(['/on-boarding' + this.router.url]);
      return false;
    }
    else {
      this.router.navigateByUrl("/login?redirection=" + state.url);
      return false;
    }
  }
}
