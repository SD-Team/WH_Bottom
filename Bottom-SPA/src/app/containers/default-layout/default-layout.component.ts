import { Component, OnInit } from '@angular/core';
import { navItems, NavItem } from '../../_nav';
import { AuthService } from '../../_core/_services/auth.service';
import { AlertifyService } from '../../_core/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {
  public sidebarMinimized = false;
  public navItems = [];
  user: any = JSON.parse(localStorage.getItem('user'));

  /**
   *
   */
  constructor(private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router,
    private nav: NavItem) {
    this.navItems = this.nav.getNav();
  }
  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('Logged out');
    window.location.href = 'https://10.4.0.48:8001/';
  }
}
