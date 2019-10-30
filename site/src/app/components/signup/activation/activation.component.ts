import { GuardGuard } from './../../../guard.guard';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { MatDialogRef, MatSnackBar } from '@angular/material';
import { AuthentificationService } from '../../../services/authentification.service';
import { LoginComponent } from '../../login/login.component';
import { Action } from 'rxjs/internal/scheduler/Action';


@Component({
  selector: 'app-activation',
  templateUrl: './activation.component.html',
  styleUrls: ['./activation.component.css']
})
export class ActivationComponent implements OnInit {

  colorMessage: string;
  message: String;
  page: string;

  constructor(
    private router: Router,
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private snackBar: MatSnackBar,
  ) { }


  ngOnInit() {
    let url = this.router.url.split('/');
    if (url[1] === "activation") {
      this.activate();
    }
  }

  activate() {
    this.activatedRoute.params.subscribe(params => {
      this.userService.activation( params.token ).subscribe(res => {
        this.message = res.message;
        this.colorMessage = 'alert alert-info';
        if (this.message === 'User Not Found') {
          this.colorMessage = 'alert alert-danger';
          this.activationSnack('Activation has failed', 'Try again');
        } else {
          this.page = 'activation';
          this.router.navigateByUrl('/login');
          this.activationSnack('Account successfully activated!', 'Go to login');
        }
      });
    });
  }

  activationSnack(message: string, action:string) {
    this.snackBar.open(message, action, {verticalPosition: 'top'});
  }

}
