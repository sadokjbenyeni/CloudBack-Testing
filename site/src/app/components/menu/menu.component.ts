import { LoginComponent } from './../login/login.component';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { MatDialog } from '@angular/material';
import { HomeComponent } from '../home/home.component';

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
    private userService: UserService,
    private matDialog: MatDialog
  ) {
    this.link = '';
    this.role = '';
  }

  ngOnInit() {
    this.role = '';
    let user = JSON.parse(sessionStorage.getItem('user'));
    if (user != null) {
      this.username = user.lastname;
    }
    if (user && typeof user === 'object') {
      this.role = user.roleName;
    } else {
      this.role = '';
    }
  }

  logout() {
    let user = JSON.parse(sessionStorage.getItem('user'));
    if (user.token) {
      this.userService.logout({ token: user.token }).subscribe(() => {
        this.role = '';
        sessionStorage.removeItem('user');
        sessionStorage.removeItem('cart');
        sessionStorage.removeItem('surveyForm');
        sessionStorage.setItem('dataset', JSON.stringify({ "dataset": "", "title": "" }));
        this.router.navigate(['/home']);
      });
    }
  }

  DoLogin():void {
    this.matDialog.open(LoginComponent);
  }
  
  ToHome()
  {
    this.router.navigateByUrl("/home")
  }

  // openNav() {
  //   document.getElementById('mySidenav').style.width = '250px';
  // }

  // closeNav() {
  //   document.getElementById('mySidenav').style.width = '0';
  // }
}
