import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-password-reset-email',
  templateUrl: './password-reset-email.component.html',
  styleUrls: ['./password-reset-email.component.css']
})
export class PasswordResetEmailComponent implements OnInit {
  email: string;
  message: String;
  constructor(
    public userService: UserService,
  ) {
  }

  ngOnInit() {
  }
  sendresetmail() {

    this.userService.mdpmail({ email: this.email }).subscribe(r => {
      if (r.mail) {
        this.message = 'An email has just been sent';
      } else {
        this.message = 'An error has occurred. Please try again';
      }
    });
  }
}
