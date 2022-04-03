import { Component, OnInit } from '@angular/core';
import {AuthService} from "../auth.service";
import {ToastrService} from "ngx-toastr";
import Utils from "../utils";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string = '';
  password: string = '';

  constructor(private authService: AuthService, private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
  }

  logIn(){
    this.authService.login(this.username, this.password)
      .subscribe((data) => {
        this.authService.saveLogin({userId:data.userId.toString(), userName:data.userName }, data.token);
        this.router.navigateByUrl('/');
      }, (error) => this.toastr.error(Utils.getErrorMessage(error),
        'Error', {timeOut: 1000, positionClass: 'toast-bottom-right' }));
  }

}
