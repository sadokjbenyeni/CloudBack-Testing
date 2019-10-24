import { AuthentificationService } from './../../services/authentification.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { MatDialog } from '@angular/material';

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
    public dialog: MatDialog,
    private authentificationService: AuthentificationService,
  ) {
    this.link = '';
    this.role = '';
  }

  ngOnInit() {
    this.authentificationService.invokeLoginComponentFunction.on('onLoginSuccessed', (loggedUser: any) => {
      this.refresh(loggedUser);
    });

    // this.refresh(this.username);
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

  ToHome() {
    this.router.navigateByUrl("/home")
  }

  refresh(loggedUser: any): void {
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

  // openNav() {
  //   document.getElementById('mySidenav').style.width = '250px';
  // }

  // closeNav() {
  //   document.getElementById('mySidenav').style.width = '0';
  // }
}


