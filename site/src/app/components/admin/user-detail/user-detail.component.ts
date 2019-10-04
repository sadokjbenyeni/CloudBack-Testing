import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';



import { UserService } from '../../../services/user.service';
import { Role } from '../../../models/Role';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

  user: { firstname: String; lastname: String; roleName: String[]; };
  userSelect: string;
  message: string;
  role: string;
  roles: Array<Role>;
  private id: string;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    route.params.subscribe(_ => this.id = _.id);
  }

  @ViewChild('utilisateurForm', { static: false })
  private userForm: NgForm;

  ngOnInit() {
    this.message = '';
    this.role = '';
    this.user = {firstname: '', lastname: '', roleName: []};
    this.userService.getRoles().subscribe(res => {
      this.roles = res.roles;
    });
    this.userService.getCompte(this.id).subscribe(res => {
      this.user = {firstname: '', lastname: '', roleName: []};
      this.user['firstname'] = res.firstname;
      this.user['lastname'] = res.lastname;
      this.user.roleName = res.roleName;
      this.user['id'] = res['_id'];
    });
  }

  roleChange(role) {
    let rolsplit = role.split('|');
    let rol = rolsplit[0];
    let rolN = rolsplit[1];
    this.user['role'] = rol;
    this.user['roleName'] = rolN;
    this.userSelect = rol + '|' + rolN;
  }  
  addRole(e) {
    this.user['roleName'].push(e);
  }
  delRole(e) {
    this.user['roleName'].splice(this.user['roleName'].indexOf(e), 1);
  }
  save() {
    this.userService.updateUser(this.user).subscribe(res => this.router.navigateByUrl('/admin/users'));
  }

}
