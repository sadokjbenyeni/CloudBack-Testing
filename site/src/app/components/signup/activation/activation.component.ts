import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { MatSnackBar } from '@angular/material';



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
          // this.activationSnack('Activation has failed', 'Try again');
        } else {
          this.page = 'activation';
          this.router.navigateByUrl('/login?activate=true');
        }
      });
    });
  }



}
