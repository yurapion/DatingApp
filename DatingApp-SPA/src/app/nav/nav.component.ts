import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/Alertify.service';
declare const myTest: any;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  
  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
   this.authService.login(this.model).subscribe(next => {
     this.alertify.success('Logged in sucessfuly');
   }, error => {
this.alertify.error(error);
   }
   );
  }

  loggedIn() {
// tslint:disable-next-line: comment-format
    //We have put the token in authService
    // Check if token exist and return true or false
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
  }

  onClick() {
    myTest();
  }

}
