import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/Alertify.service';
import {Router} from '@angular/router';
declare const myTest: any;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  photoUrl: string;

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  login() {
   this.authService.login(this.model).subscribe(next => {
     this.alertify.success('Logged in sucessfuly');
   }, error => {
this.alertify.error(error);
   }, () => {
     this.router.navigate(['/members']);
   });
  }

  loggedIn() {
// tslint:disable-next-line: comment-format
    //We have put the token in authService
    // Check if token exist and return true or false
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

  onClick() {
    myTest();
  }

}
