import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/User';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/Alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
// tslint:disable-next-line: component-selector
  selector: 'app-member_list',
  templateUrl: './member_list.component.html',
  styleUrls: ['./member_list.component.css']
})
// tslint:disable-next-line: class-name
export class Member_listComponent implements OnInit {
users: User[];
  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'];
    });
  }

  // loadUsers() {
  //   this.userService.getUsers().subscribe((users: User[]) => {
  //     this.users = users;
  //   }, error => {
  //     this.alertify.error(error);
  //   });

  // }

}
