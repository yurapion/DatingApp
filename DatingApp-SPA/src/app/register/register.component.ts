import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/Alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 
  @Output() cancelRegister = new EventEmitter();
  @Output() modelToHome = new EventEmitter();
model: any = {};
  constructor(private authServide: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register(){
   this.authServide.register(this.model).subscribe(() => {
    this.alertify.success('register success');
    /* put model data to edit box*/
    this.cancelRegister.emit(false);
   }, error => { 
     this.alertify.error(error);
   });
  }

  cancel() {
 this.cancelRegister.emit(false);
 this.alertify.message('register canceled ');
  }
}
