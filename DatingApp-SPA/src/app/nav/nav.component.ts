import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
   this.authService.login(this.model).subscribe(next => {
     console.log('Logged in sucessfuly');
   }, error => {
console.log(error);
   }
   );
  }

  loggedIn() {
// tslint:disable-next-line: comment-format
    //We have put the token in authService
    const token = localStorage.getItem('token');
    // Check if token exist and return true or false
    return !!token;
  }

  logout(){
    localStorage.removeItem('token');
    console.log('logged out');
  }


}
