import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthService } from './_core/_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  // tslint:disable-next-line
  selector: "body",
  template: "<router-outlet></router-outlet>"
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit() {
    this.router.events.subscribe(evt => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
    const token = localStorage.getItem("token");
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
