import { Component, OnInit, AfterViewChecked, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subject } from 'rxjs/Subject';



import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit, AfterViewChecked {

  message: string;
  users: Array<object>;
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();

  constructor(
    private userService: UserService
  ) { }

  @ViewChild('utilisateurForm', { static: false })
  private userForm: NgForm;

  ngOnInit() {
    this.message = '';

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };

    this.userService.getUsers().subscribe(res => {
      this.users = res.users;
      this.dtTrigger.next();
    });
  }

  ngAfterViewChecked() {

  }

  add() {
  }
  edit(id): void {
  }

}
