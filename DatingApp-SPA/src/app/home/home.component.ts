import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AlertifyService } from '../_services/Alertify.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
registerMode = false;


  constructor(private http: HttpClient,  private alertify: AlertifyService) { }

  ngOnInit() {
   
  }

  registerToggle() {
    this.registerMode = true;
  }


  cancelRegisterMode(registerMode: boolean) {
this.registerMode = registerMode;
  }
 

}
