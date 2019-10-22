import { LoginComponent } from './../login/login.component';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, CanActivate } from '@angular/router';

import { UserService } from '../../services/user.service';
import { MatDialog, MatDialogRef } from '@angular/material';
import { HomeComponent } from '../home/home.component';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  link: any;
  role: string;
  username: string;
  currentDialog: MatDialogRef<any> = null;
  destroy = new Subject<any>();


  constructor(
    private router: Router,
    private userService: UserService,
    private matDialog: MatDialog,
    private route: ActivatedRoute
  ) {
    this.link = '';
    this.role = '';
    
  }

  ngOnDestroy() {
    this.destroy.next();
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

  // DoLogin(): void {
  //   this.matDialog.open(LoginComponent);
  // }

  ToHome() {
    this.router.navigateByUrl("/home")
  }

  // openNav() {
  //   document.getElementById('mySidenav').style.width = '250px';
  // }

  // closeNav() {
  //   document.getElementById('mySidenav').style.width = '0';
  // }

  login():void {
    this.route.params.pipe(takeUntil(this.destroy))
      .subscribe(params => {
        if (this.currentDialog) {
          this.currentDialog.close();
        }
        this.currentDialog = this.matDialog.open(LoginComponent, {
          data: { id: params.id }
        })
        this.currentDialog.afterClosed().subscribe(result => {
          this.router.navigateByUrl('/');
        })
      })
  }

}

