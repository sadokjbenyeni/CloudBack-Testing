import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { UserService } from '../../services/user.service';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from '../login/login-dialog/login-dialog.component';
import * as jwt_decode from 'jwt-decode';
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  link: any;
  role: string;
  username: string;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private userService: UserService
  ) {
    this.link = '';
    this.role = '';
  }

  ngOnInit() {
    this.userService.invokeLoginComponentFunction.on('onLoginSuccessed', () => {
      this.refresh();
    });

    this.refresh();
  }

  logout() {
    var token = jwt_decode(localStorage.getItem('token'))['token']
    if (token) {
      this.userService.logout(token).subscribe(() => {
        this.role = '';
        localStorage.removeItem('token');
        this.router.navigateByUrl('/home');
      });
    }
  }

  ToHome() {
    this.router.navigateByUrl("/home")
  }

  refresh(): void {
    this.role = '';
    let token = localStorage.getItem('token');
    if (token != null) {
      let data = jwt_decode(token);
      this.username = data.lastname;
      this.role = data['roleName'];
    }
  }

  openDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      panelClass: 'no-padding-dialog',
      data: { source: this.route.parent.url }
    });
  }
  goto(element) {
    if (this.router.url.includes("/on-boarding")) {
      element = document.getElementById(element).scrollIntoView({ behavior: 'smooth' });
    }
  }

}


