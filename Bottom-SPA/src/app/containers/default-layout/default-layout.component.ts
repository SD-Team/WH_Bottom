import {Component } from '@angular/core';
import { navItems } from '../../_nav';
import { AuthService } from '../../_core/_services/auth.service';
import { AlertifyService } from '../../_core/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: "app-dashboard",
  templateUrl: "./default-layout.component.html"
})
export class DefaultLayoutComponent {
  public sidebarMinimized = false;
  public navItems = navItems;

  /**
   *
   */
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) {
    
    
  }
  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message("Logged out");
    this.router.navigate(["/login"]);
  }
}
