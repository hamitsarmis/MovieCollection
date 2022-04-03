import {Component, OnInit} from '@angular/core';
import {AuthService} from "./auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Movie Collections';
  loggedInUserName = '';
  isUserLoggedIn = false;
  constructor(private authService: AuthService, private router: Router) {  }

  ngOnInit(): void {
    this.authService.isUserLoggedIn.subscribe((data) => {
      this.isUserLoggedIn = data;
      if (data === true) {
        this.loggedInUserName = this.authService.getUser().userName;
      }
    });
  }

  logoutUser() {
    this.authService.logOff();
    this.router.navigateByUrl('/');
  }
}
