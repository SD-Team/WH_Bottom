import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(private authService: AuthService, private router: Router) {}
  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }
    else {
      window.location.href = 'http://10.1.0.18/LeaveIntegration/Public/AutoLoginHandler.ashx';
    }
  }
}
