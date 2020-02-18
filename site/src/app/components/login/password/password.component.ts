import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']
})
export class PasswordComponent implements OnInit {
 email : string;
 message : String;
  constructor(
    public userService: UserService,
  ) {
  }

  ngOnInit(){
  }
  sendresetmail()
  {
    this.userService.verifmail({ email: this.email }).subscribe(res => {
      if (!res.valid) {
        this.message = res.message;
      } else {
        this.userService.mdpmail({ email:this.email, token: res.token }).subscribe(r => {
          if (r.mail) {
            this.message = 'An email has just been sent';
          } else {
            this.message = 'An error has occurred. Please try again';
          }
        });
      }
    });
  }
}
