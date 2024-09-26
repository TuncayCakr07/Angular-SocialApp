import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { User } from '../../_models/user';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  users: User[];
  public loading = false;
  userParams: any = {};

  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.loading = true;
    console.log(this.userParams);
    this.userService.getUsers(null,this.userParams).subscribe(user => {
      this.loading = false;
      this.users = user;
    }, err => {
      this.loading = false;
      this.alertify.error(err);
    })
  }

}
