import { Component, OnInit } from '@angular/core';
import { RoleService } from '../../../services/role.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  roles: Array<object>;
  pages: Array<string>;
  constructor(private roleService: RoleService) {
  }

  ngOnInit() {
    this.getRoles();
    this.getPages();
  }

  getRoles() {
    this.roleService.getRoles().subscribe(res => this.roles = res.roles);
  }

  getPages() {
    this.roleService.getPages().subscribe(res => this.pages = res.pages);
  }

}
